
Namespace AzTool
''' <summary>
''' 进度条类
''' </summary>
''' <remarks></remarks>
Public Class AzProgress

    ''' <summary>
    ''' 指示在线程执行过程中是否重置子进度条
    ''' </summary>
    Private isResetMainProgress, isResetSubProgress As Boolean
    ''' <summary>
    ''' 进度条是否启动
    ''' </summary>
    Private Property IsRun As Boolean = False
    ''' <summary>
    ''' 获取或设置一个值指示是否需要显示子进度条
    ''' </summary>
    Public Property ShowSubProgress As Boolean
    ''' <summary>
    ''' 获取或设置主进度条的提示内容
    ''' </summary>
    Public Property TipMainProgress As String
    ''' <summary>
    ''' 获取或设置子进度条的提示内容
    ''' </summary>
    Public Property TipSubProgress As String
    ''' <summary>
    ''' 当前子进度项目计数
    ''' </summary>
    Private Property CurSubItemCount As Integer
    ''' <summary>
    ''' 主进度条的项目数
    ''' </summary>
    Private Property MainProgressItemCount As Integer
    ''' <summary>
    ''' 子进度条项目数,设置该属性会初始子化进度条控件
    ''' </summary>
    Private Property SubProgressItemCount As Integer
    ''' <summary>
    ''' 当前主进度条的值
    ''' </summary>
    Private Property MainProgressValue As Integer
    ''' <summary>
    ''' 当前子进度条的值
    ''' </summary>
    Private Property SubProgressValue As Integer
    ''' <summary>
    ''' 进度条样式
    ''' </summary>
    Public Property ProgressStyle As ProgressBarStyle = ProgressBarStyle.Blocks

    ''' <summary>
    ''' 只显示主进度条的线程
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ProgressThread1()
        Dim frmProgress As New frmProgress
        frmProgress.pgbMain.Value = 0
        frmProgress.pgbMain.Style = Me.ProgressStyle
        frmProgress.pgbMain.Maximum = Me.MainProgressItemCount
        frmProgress.Size = New Size(frmProgress.Size.Width, 70)
        frmProgress.Show()
        While Me.IsRun
            If Me.isResetMainProgress Then
                Me.isResetMainProgress = False
                frmProgress.pgbMain.Maximum = Me.MainProgressItemCount
            End If
            frmProgress.pgbMain.Value = Me.MainProgressValue
            frmProgress.lblPrompt1.Text = Me.TipMainProgress
            Threading.Thread.Sleep(100)
            System.Windows.Forms.Application.DoEvents()
        End While
        frmProgress.Dispose()
    End Sub

    ''' <summary>
    ''' 显示主进度条和子进度条的线程
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ProgressThread2()
        Dim frmProgress As New frmProgress
        frmProgress.pgbSub.Value = 0
        frmProgress.pgbSub.Style = Me.ProgressStyle
        frmProgress.pgbMain.Value = 0
        frmProgress.pgbMain.Style = Me.ProgressStyle
        frmProgress.pgbMain.Maximum = Me.MainProgressItemCount
        frmProgress.Size = New Size(frmProgress.Size.Width, 125)
        frmProgress.Show()
        While Me.IsRun
            If Me.isResetMainProgress Then
                Me.isResetMainProgress = False
                frmProgress.pgbMain.Maximum = Me.MainProgressItemCount
            End If
            frmProgress.pgbMain.Value = Me.MainProgressValue
            frmProgress.pgbSub.Value = Me.SubProgressValue
            frmProgress.lblPrompt1.Text = Me.TipMainProgress
            frmProgress.lblPrompt2.Text = Me.TipSubProgress
            Threading.Thread.Sleep(100)
            System.Windows.Forms.Application.DoEvents()
        End While
        Me.SubProgressItemCount = 0
        Me.MainProgressItemCount = 0
        frmProgress.Dispose()
    End Sub

    ''' <summary>
    ''' 启用进度条对象，该过程会对进度条进行初始化操作
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub StartProgress()
        Me.IsRun = True
        Me.isResetMainProgress = True
        Me.isResetSubProgress = True
        Dim t As Threading.Thread
        If Me.ShowSubProgress Then
            t = New Threading.Thread(AddressOf ProgressThread2)
        Else
            t = New Threading.Thread(AddressOf ProgressThread1)
        End If
        t.Start()
    End Sub

    ''' <summary>
    ''' 移动主进度条到下一个位置
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub StepNextMainProgress()
        Me.MainProgressValue += 1
    End Sub

    ''' <summary>
    ''' 移动子进度条到下一个位置
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub StepNextSubProgress()
        Me.CurSubItemCount += 1
        Me.SubProgressValue = CInt(Me.CurSubItemCount / Me.SubProgressItemCount * 100)
    End Sub

    ''' <summary>
    ''' 初始化主进度条
    ''' </summary>
    ''' <param name="mainProgressItemCount">主进度条项目数</param>
    ''' <remarks></remarks>
    Public Sub InitMainProgress(ByVal mainProgressItemCount As Integer)
        Me.MainProgressValue = 0
        Me.isResetMainProgress = True
        Me.MainProgressItemCount = mainProgressItemCount
    End Sub

    ''' <summary>
    ''' 初始化子进度条
    ''' </summary>
    ''' <param name="subProgressItemCount">子进度条项目数</param>
    ''' <remarks></remarks>
    Public Sub InitSubProgress(ByVal subProgressItemCount As Integer)
        Me.CurSubItemCount = 0
        Me.SubProgressValue = 0
        Me.isResetSubProgress = True
        Me.SubProgressItemCount = subProgressItemCount
    End Sub

    ''' <summary>
    ''' 停用进度条
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub StopProgress()
        Me.IsRun = False
        Me.ShowSubProgress = False
    End Sub

    End Class
End Namespace
