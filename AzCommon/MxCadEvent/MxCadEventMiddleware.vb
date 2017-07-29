
''' <summary>
''' 处理CAD事件的中间件
''' </summary>
''' <remarks></remarks>
Public Class MxCadEventMiddleware
    ''' <summary>
    ''' 当前命令ID值
    ''' </summary>
    Private Property curCommandId As Short

    ''' <summary>
    ''' 空命令ID值
    ''' </summary>
    Private Const emptyCommandId As Short = -1000

    ''' <summary>
    ''' CAD事件处理对象
    ''' </summary>
    Private Property cadEventObj As MxCadEvent

    ''' <summary>
    ''' 下一个CAD事件处理器对象
    ''' </summary>
    Private Property nextEventObj As MxCadEvent

    ''' <summary>
    ''' 应用程序主窗体，一般指主CAD控件所在的窗体对象
    ''' </summary>
    Private mainForm As Windows.Forms.Form

    ''' <summary>
    ''' 执行事件的CAD对象
    ''' </summary>
    Private Property mxCadObj As AxMxDrawXLib.AxMxDrawX

    Public Sub New(ByRef cadObj As AxMxDrawXLib.AxMxDrawX)
        mxCadObj = cadObj
        Dim parentObj = mxCadObj.Parent
        Do Until TypeOf parentObj Is Windows.Forms.Form
            parentObj = parentObj.Parent
        Loop
        mainForm = CType(parentObj, Windows.Forms.Form)
        AddEventCorrelation()
    End Sub

    ''' <summary>
    ''' 设置事件对象关联
    ''' </summary>
    ''' <param name="eventObj">待设置事件对象,单对象为Nothing时代表空状态</param>
    ''' <remarks></remarks>
    Public Sub SetCadEventObj(ByVal eventObj As MxCadEvent)
        If Me.cadEventObj Is Nothing Then
            If eventObj IsNot Nothing Then
                Me.cadEventObj = eventObj
                'AddEventCorrelation()
                Me.cadEventObj.SetEventoObjAndMainForm(mainForm, mxCadObj)
                Me.cadEventObj.Init()
            End If
        Else
            Me.cadEventObj.CommandEnded("")
            Me.mxCadObj.DoCommand(MxCadEventMiddleware.emptyCommandId) '空命令
            If eventObj Is Nothing Then
                Me.cadEventObj = Nothing
            Else
                Me.nextEventObj = eventObj
            End If
        End If
    End Sub

    ''' <summary>
    ''' 设置CAD控件事件关联
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddEventCorrelation()
        AddHandler mxCadObj.CommandWillStart, AddressOf CommandWillStart
        AddHandler mxCadObj.ImplementCommandEvent, AddressOf ImplementCommandEvent
        AddHandler mxCadObj.DynWorldDraw, AddressOf DynWorldDraw
        AddHandler mxCadObj.MouseEvent, AddressOf MouseEvent
        AddHandler mxCadObj.MxKeyDown, AddressOf MxKeyDown
        AddHandler mxCadObj.CommandEnded, AddressOf CommandEnded
    End Sub

    ''' <summary>
    ''' 取消CAD控件事件关联
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CancelEventCorrelation()
        RemoveHandler mxCadObj.CommandWillStart, AddressOf CommandWillStart
        RemoveHandler mxCadObj.ImplementCommandEvent, AddressOf ImplementCommandEvent
        RemoveHandler mxCadObj.DynWorldDraw, AddressOf DynWorldDraw
        RemoveHandler mxCadObj.MouseEvent, AddressOf MouseEvent
        RemoveHandler mxCadObj.MxKeyDown, AddressOf MxKeyDown
        RemoveHandler mxCadObj.CommandEnded, AddressOf CommandEnded
    End Sub

    ''' <summary>
    ''' 命令将要开始执行时事件
    ''' </summary>
    ''' <param name="e">待执行的命令参数，包含待执行的命令名称</param>
    ''' <remarks></remarks>
    Private Sub CommandWillStart(ByVal sender As Object, ByVal e As AxMxDrawXLib._DMxDrawXEvents_CommandWillStartEvent)
        If Me.cadEventObj IsNot Nothing Then
            If e.sCmdName = "IntelliSel" Then
                Exit Sub
            ElseIf e.sCmdName = "MXOCXSYS_ImpMxDrawXCommand" Then
                cadEventObj.CommandWillStart(e.sCmdName)
            Else
                Me.cadEventObj = Nothing
                'CancelEventCorrelation()
            End If
        End If
    End Sub

    ''' <summary>
    ''' 执行命令事件
    ''' </summary>
    Private Sub ImplementCommandEvent(ByVal sender As System.Object, ByVal e As AxMxDrawXLib._DMxDrawXEvents_ImplementCommandEventEvent)
        curCommandId = e.iCommandId
        If cadEventObj IsNot Nothing AndAlso e.iCommandId <> MxCadEventMiddleware.emptyCommandId Then
            cadEventObj.ImplementCommandEvent(e)
        End If
    End Sub

    ''' <summary>
    ''' 动态拖放时的绘制事件,即屏幕绘制
    ''' </summary>
    ''' <param name="e">动态绘制参数</param>
    ''' <remarks></remarks>
    Private Sub DynWorldDraw(ByVal sender As Object, ByVal e As AxMxDrawXLib._DMxDrawXEvents_DynWorldDrawEvent)
        If cadEventObj IsNot Nothing Then
            cadEventObj.DynWorldDraw(e)
        End If
    End Sub

    ''' <summary>
    ''' 处理鼠标事件
    ''' </summary>
    ''' <param name="e">事件参数，包含事件发生时的坐标信息，鼠标事件类型等</param>
    Private Sub MouseEvent(ByVal sender As Object, ByVal e As AxMxDrawXLib._DMxDrawXEvents_MouseEventEvent)
        If cadEventObj IsNot Nothing Then
            Select Case e.lType
                Case 1
                    cadEventObj.MoveToolTip(e.dX, e.dY)
                    cadEventObj.MouseMove(e.dX, e.dY)
                Case 2
                    cadEventObj.MouseLeftDown(e.dX, e.dY)
                Case 3
                    cadEventObj.MouseRightDown(e.dX, e.dY)
            End Select
        End If
    End Sub

    ''' <summary>
    ''' 处理键盘键按下事件
    ''' </summary>
    ''' <param name="e">事件参数，包含事件发生时的按键相关信息</param>
    ''' <remarks></remarks>
    Private Sub MxKeyDown(ByVal sender As Object, ByVal e As AxMxDrawXLib._DMxDrawXEvents_MxKeyDownEvent)
        If cadEventObj IsNot Nothing Then
            cadEventObj.KeyDown(e.lVk)
        End If
    End Sub

    ''' <summary>
    ''' 命令执行完成事件
    ''' </summary>
    ''' <param name="e">事件参数，包含已执行完成的命令名称</param>
    ''' <remarks></remarks>
    Private Sub CommandEnded(ByVal sender As Object, ByVal e As AxMxDrawXLib._DMxDrawXEvents_CommandEndedEvent)
        If cadEventObj IsNot Nothing AndAlso Me.curCommandId <> MxCadEventMiddleware.emptyCommandId Then
            cadEventObj.CommandEnded(e.sCmdName)
            'cadEventObj.SetLastCommandId(curCommandId)
        End If
        If Me.nextEventObj IsNot Nothing Then '初始化下一个CAD事件处理器对象
            'If Me.mxCadObj IsNot Nothing Then
            '    CancelEventCorrelation()
            'End If
            Me.cadEventObj = Me.nextEventObj
            Me.cadEventObj.SetEventoObjAndMainForm(mainForm, mxCadObj)
            Me.cadEventObj.Init()
            Me.nextEventObj = Nothing
            'AddEventCorrelation()
        End If
    End Sub

    ' ''' <summary>
    ' ''' 在NET释放对象时取消事件关联
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Protected Overrides Sub Finalize()
    '    CancelEventCorrelation()
    'End Sub

End Class
