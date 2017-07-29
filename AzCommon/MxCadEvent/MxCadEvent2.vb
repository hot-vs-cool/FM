
''' <summary>
''' CAD事件处理对象类
''' </summary>
''' <remarks></remarks>
Public Class MxCadEvent2

    ''' <summary>
    ''' 指示是否处于待执行下一个命令状态
    ''' </summary>
    Public Property IsInNextCommand As Boolean

    ''' <summary>
    ''' 应用程序主窗体
    ''' </summary>
    Protected Property mainForm As Windows.Forms.Form

    ''' <summary>
    ''' 事件关联的CAD对象
    ''' </summary>
    Protected mxCadObj As AxMxDrawXLib.AxMxDrawX

    ''' <summary>
    ''' 通过CAD控件对象和应用程序主窗体初始化事件管理对象
    ''' </summary>
    ''' <param name="cadObj">CAD控件对象</param>
    ''' <param name="mainForm">应用程序主窗体</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal cadObj As AxMxDrawXLib.AxMxDrawX, ByVal mainForm As Windows.Forms.Form)
        Me.mxCadObj = cadObj
        Me.mainForm = mainForm
    End Sub

    ''' <summary>
    ''' 把当前状态做一个UNDO标记，方便回退到这个标记
    ''' </summary>
    Protected Sub MarkUndo()
        Me.mxCadObj.Cal("Mx_UndoMark")
    End Sub

    ''' <summary>
    ''' 命令执行前的初始化,该过程作为“ImplementCommandEvent”的子过程触发
    ''' </summary>
    ''' <param name="isCancel">获取或设置是否取消接下来的事件处理</param>
    ''' <remarks></remarks>
    Protected Overridable Sub CommandStartInit(ByRef isCancel As Boolean)

    End Sub

    ''' <summary>
    ''' 命令执行中的代码块，该过程作为“ImplementCommandEvent”的子过程触发
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub CommandRunning(ByVal iCommandId As Short)

    End Sub

    ''' <summary>
    ''' 命令执行结束时处理过程，该过程作为“ImplementCommandEvent”的子过程触发
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub CommandEndedHandle()

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
    Public Sub ImplementCommandEvent(ByVal e As AxMxDrawXLib._DMxDrawXEvents_ImplementCommandEventEvent)
        Me.IsInNextCommand = False
        Dim isCancle As Boolean = False
        Me.CommandStartInit(isCancle)
        If isCancle Then Exit Sub '取消接下来的事件处理
        Me.CommandRunning(e.iCommandId)
        Me.CommandEndedHandle()
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
    ''' 释放功能模块所使用资源,一般在功能模块切换时调用该方法
    ''' </summary>
    ''' <remarks></remarks>
    Public Overridable Sub Dispose()

    End Sub

End Class
