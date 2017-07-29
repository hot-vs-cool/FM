
''' <summary>
''' CAD事件处理类
''' </summary>
''' <remarks></remarks>
Public MustInherit Class MxCadEvent

    ''' <summary>
    ''' CAD界面随光标移动的提示标签
    ''' </summary>
    Private lblTooltip As New Windows.Forms.Label

    ''' <summary>
    ''' 动态提示中CAD命令提示内容部分
    ''' </summary>
    Private _commandTipMsg As String = ""

    ''' <summary>
    ''' 动态提示中系统信息提示内容部分
    ''' </summary>
    Private _sysInfoTipMsg As String = ""

    Private _isUseToolTip As Boolean = False

    ''' <summary>
    ''' 上一次执行的命令ID值
    ''' </summary>
    Private _lastCommandId As Short = -1

    ''' <summary>
    ''' 应用程序主窗体
    ''' </summary>
    Protected Property mainForm As Windows.Forms.Form

    ''' <summary>
    ''' 事件关联的CAD对象
    ''' </summary>
    Protected mxCadObj As AxMxDrawXLib.AxMxDrawX

    ''' <summary>
    ''' 获取或设置CAD界面随光标移动的提示标签的内容,该内容为CAD命令提示部分
    ''' </summary>
    Public Property CommandTipMsg As String
        Get
            Return _commandTipMsg
        End Get
        Set(ByVal value As String)
            _commandTipMsg = value.Trim
            Me.lblTooltip.Text = _sysInfoTipMsg & _commandTipMsg
            If Me._isUseToolTip AndAlso Me.lblTooltip.Text <> "" Then
                Me.lblTooltip.Visible = True
            Else
                Me.lblTooltip.Visible = False
            End If
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置CAD界面随光标移动的提示标签的内容,该内容为系统信息部分
    ''' </summary>
    Public Property SysInfoTipMsg As String
        Get
            Return _sysInfoTipMsg
        End Get
        Set(ByVal value As String)
            _sysInfoTipMsg = value.Trim
            Me.lblTooltip.Text = _sysInfoTipMsg & _commandTipMsg
            If Me._isUseToolTip AndAlso Me.lblTooltip.Text <> "" Then
                Me.lblTooltip.Visible = True
            Else
                Me.lblTooltip.Visible = False
            End If
        End Set
    End Property

    ''' <summary>
    ''' 指示是否启用随光标移动的标签提示;"True"为启用,"False"不启用
    ''' </summary>
    ''' <remarks>当标签内容不为空时，该属性值设置为"True"才会生效</remarks>
    Public Property IsUseToolTip As Boolean
        Get
            Return _isUseToolTip
        End Get
        Set(ByVal value As Boolean)
            _isUseToolTip = value
            If Not _isUseToolTip Then
                Me.lblTooltip.Visible = False
            End If
            If _isUseToolTip AndAlso lblTooltip.Text <> "" Then
                lblTooltip.Visible = True
            End If
        End Set
    End Property

    ''' <summary>
    ''' 上一次执行命令的ID值,值为-1表示未执行任何命令
    ''' </summary>
    Public ReadOnly Property LastCommandId As Short
        Get
            Return _lastCommandId
        End Get
    End Property

    ''' <summary>
    ''' 本过程包含初始化CAD截面随光标移动的标签控件
    ''' </summary>
    Public Sub New()
        lblTooltip.Visible = False
        lblTooltip.AutoSize = True
        lblTooltip.Location = New Drawing.Point(200, 150)
        lblTooltip.Font = New Drawing.Font("宋体", 10)
        lblTooltip.BackColor = Drawing.Color.FromArgb(22, 20, 87)
        lblTooltip.ForeColor = Drawing.Color.FromArgb(199, 199, 220)
        lblTooltip.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
    End Sub

    ''' <summary>
    ''' 对象初始化，一般用于功能模块开始启用前对操作界面进行初始化
    ''' </summary>
    ''' <remarks></remarks>
    Public Overridable Sub Init()

    End Sub

    ''' <summary>
    ''' 设置与事件关联的CAD和应用程序主窗体,该方法一般由CAD控件事件中间件对象调用
    ''' </summary>
    ''' <param name="frmMain">应用程序主窗体</param>
    ''' <param name="mxCadObj">与事件关联的CAD对象</param>
    ''' <remarks></remarks>
    Friend Sub SetEventoObjAndMainForm(ByRef frmMain As Windows.Forms.Form, ByRef mxCadObj As AxMxDrawXLib.AxMxDrawX)
        Me.mainForm = frmMain
        Me.mxCadObj = mxCadObj
        Me.mainForm.Controls.Add(Me.lblTooltip)
        Me.lblTooltip.BringToFront() '设置标签顶级显示
    End Sub

    ''' <summary>
    ''' 设置CAD事件处理对象最近一次的执行的命令ID值,该方法一般由CAD控件事件中间件对象调用
    ''' </summary>
    ''' <param name="commandId">最近一次的命令ID值</param>
    Friend Sub SetLastCommandId(ByVal commandId As Short)
        Me._lastCommandId = commandId
    End Sub

    ''' <summary>
    ''' 移动动态提示标签到指定位置,该方法一般由CAD控件事件中间件对象调用
    ''' </summary>
    ''' <param name="dx">动态提示框左上角X坐标值,一般取绘图区域光标位置X坐标值</param>
    ''' <param name="dy">动态提示框左上角Y坐标值,一般取绘图区域光标位置Y坐标值</param>
    ''' <remarks></remarks>
    Friend Sub MoveToolTip(ByVal dx As Double, ByVal dy As Double)
        If IsUseToolTip AndAlso lblTooltip.Text <> "" Then
            Dim srcPoint = mxCadObj.DocToScreenCoord(dx, dy)
            Me.lblTooltip.Location = New Drawing.Point(CInt(srcPoint.x) + 20, CInt(srcPoint.y))
        End If
    End Sub

    ''' <summary>
    ''' 把当前状态做一个UNDO标记，方便回退到这个标记
    ''' </summary>
    Protected Sub MarkUndo()
        Me.mxCadObj.Cal("Mx_UndoMark")
    End Sub

    ''' <summary>
    ''' 命令将要开始执行时事件
    ''' </summary>
    ''' <param name="commandName">待执行的命令名称</param>
    ''' <remarks></remarks>
    Public Overridable Sub CommandWillStart(ByVal commandName As String)

    End Sub

    ''' <summary>
    ''' 命令执行事件
    ''' </summary>
    ''' <param name="e">命令执行事件参数，包含命令ID编号</param>
    ''' <remarks></remarks>
    Public Overridable Sub ImplementCommandEvent(ByVal e As AxMxDrawXLib._DMxDrawXEvents_ImplementCommandEventEvent)

    End Sub

    ''' <summary>
    ''' 动态拖放时的绘制事件,即屏幕绘制
    ''' </summary>
    ''' <param name="e">动态绘制参数</param>
    ''' <remarks></remarks>
    Public Overridable Sub DynWorldDraw(ByVal e As AxMxDrawXLib._DMxDrawXEvents_DynWorldDrawEvent)

    End Sub

    ''' <summary>
    ''' 处理鼠标移动事件
    ''' </summary>
    ''' <param name="dx">事件发生时鼠标X坐标</param>
    ''' <param name="dy">事件发生时鼠标Y坐标</param>
    ''' <remarks></remarks>
    Public Overridable Sub MouseMove(ByVal dx As Double, ByVal dy As Double)

    End Sub

    ''' <summary>
    ''' 处理鼠标左键按下事件
    ''' </summary>
    ''' <param name="dx">事件发生时鼠标X坐标</param>
    ''' <param name="dy">事件发生时鼠标Y坐标</param>
    ''' <remarks></remarks>
    Public Overridable Sub MouseLeftDown(ByVal dx As Double, ByVal dy As Double)

    End Sub

    ''' <summary>
    ''' 处理鼠标右键按下事件
    ''' </summary>
    ''' <param name="dx">事件发生时鼠标X坐标</param>
    ''' <param name="dy">事件发生时鼠标Y坐标</param>
    ''' <remarks></remarks>
    Public Overridable Sub MouseRightDown(ByVal dx As Double, ByVal dy As Double)

    End Sub

    ''' <summary>
    ''' 处理键盘按键事件
    ''' </summary>
    ''' <param name="key">事件发生时的按键值枚举</param>
    ''' <remarks></remarks>
    Public Overridable Sub KeyDown(ByVal key As Integer)

    End Sub

    ''' <summary>
    ''' CAD一个命令执行结束事件
    ''' </summary>
    ''' <param name="commandName">待执行的命令名称</param>
    ''' <remarks></remarks>
    Public Overridable Sub CommandEnded(ByVal commandName As String)

    End Sub

    ''' <summary>
    ''' 功能模块结束事件，一般在此执行窗体关闭、恢复操作界面默认等操作
    ''' </summary>
    ''' <remarks></remarks>
    Public Overridable Sub FunctionEnded()

    End Sub

End Class
