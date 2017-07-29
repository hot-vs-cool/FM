
''' <summary>
''' 主界面功能标记和命令ID枚举
''' </summary>
Public Enum FunctionTag As Short
    '共用功能模块
    ''' <summary>
    ''' CAD图纸导入
    ''' </summary>
    DrawingInto
    ''' <summary>
    ''' 设备识别
    ''' </summary>
    RecognizeDevice
    ''' <summary>
    ''' 属性编辑
    ''' </summary>
    ''' <remarks></remarks>
    ComponentPropertyEdit
    ''' <summary>
    ''' 构件删除
    ''' </summary>
    ComponentDelete
    ''' <summary>
    ''' 图纸局部独显
    ''' </summary>
    ShowDrawingAlong
    ''' <summary>
    ''' 构件套数设置
    ''' </summary>
    SetComponentMultiple
    ''' <summary>
    ''' 图块基点修正
    ''' </summary>
    CorrectBasePt

    '电气工程工具条
    ''' <summary>
    ''' 构件定义
    ''' </summary>
    ComponentDefine
    ''' <summary>
    ''' 设置电气专业回路信息
    ''' </summary>
    SetElecCircuit
    ''' <summary>
    ''' 自动接线盒
    ''' </summary>
    CreateJunctionBox
    ''' <summary>
    ''' 电气工程回路绘制
    ''' </summary>
    DrawElecCircuit
    ''' <summary>
    ''' 电气工程桥架绘制
    ''' </summary>
    DrawElecBridge
    ''' <summary>
    ''' 防雷接地设备绘制
    ''' </summary>
    DrawThunderComponent

    '水暖工程工具条
    ''' <summary>
    ''' 水暖工程系统设置
    ''' </summary>
    SetPlumbingSystem
    ''' <summary>
    ''' 水暖工程管道绘制
    ''' </summary>
    DrawPlumbingPipe
    ''' <summary>
    ''' 水暖工程管道拾取
    ''' </summary>
    PickPlumbingPipe
    ''' <summary>
    ''' 水暖工程喷淋头识别
    ''' </summary>
    RecoSprinkler
    ''' <summary>
    ''' 水暖工程喷淋管识别
    ''' </summary>
    RecoSprinklerPipe

    '通风空调工程
    ''' <summary>
    ''' 设置通风空调系统
    ''' </summary>
    SetHVACSystem
    ''' <summary>
    ''' 风管绘制
    ''' </summary>
    DrawAirDuct
    ''' <summary>
    ''' 空调水管道绘制
    ''' </summary>
    DrawAirConditionPipe
    ''' <summary>
    ''' 空调水管道拾取
    ''' </summary>
    PickAirConditionPipe
    ''' <summary>
    ''' 风管配件布置
    ''' </summary>
    DrawAirDuctFitting


    '图层控制工具条
    ''' <summary>
    ''' 设置指定图层的不可见
    ''' </summary>
    SetLayerUnVisible
    ''' <summary>
    ''' 设置处指定图层外其它图层的不可见
    ''' </summary>
    SetOtherLayerUnVisisble
    ''' <summary>
    ''' 设置所有图层的可见
    ''' </summary>
    SetAllLayerVisible
    ''' <summary>
    ''' 设置构件图层的可见性
    ''' </summary>
    SetCptLayerVisible
    '其它
    ''' <summary>
    ''' 执行空命令
    ''' </summary>
    DoEmptyCommand
    ''' <summary>
    ''' 内部测试功能模块1
    ''' </summary>
    FunctionTest1
    ''' <summary>
    ''' 内部测试功能模块2
    ''' </summary>
    FunctionTest2
    ''' <summary>
    ''' 执行系统内置命令
    ''' </summary>
    DoSystemCommand
    ''' <summary>
    ''' 退出主程序
    ''' </summary>
    ExitApplication
End Enum

''' <summary>
''' 中国省份地区枚举
''' </summary>
''' <remarks></remarks>
Public Enum Province
    北京市 = 110000
    天津市 = 120000
    河北省 = 130000
    山西省 = 140000
    内蒙古自治区 = 150000
    辽宁省 = 210000
    吉林省 = 220000
    黑龙江省 = 230000
    上海市 = 310000
    江苏省 = 320000
    浙江省 = 330000
    安徽省 = 340000
    福建省 = 350000
    江西省 = 360000
    山东省 = 370000
    河南省 = 410000
    湖北省 = 420000
    湖南省 = 430000
    广东省 = 440000
    广西壮族自治区 = 450000
    海南省 = 460000
    重庆市 = 500000
    四川省 = 510000
    贵州省 = 520000
    云南省 = 530000
    西藏自治区 = 540000
    陕西省 = 610000
    甘肃省 = 620000
    青海省 = 630000
    宁夏回族自治区 = 640000
    新疆维吾尔自治区 = 650000
    台湾省 = 710000
    香港特别行政区 = 810000
    澳门特别行政区 = 820000
End Enum

''' <summary>
''' 布尔是否枚举
''' </summary>
''' <remarks></remarks>
Public Enum YesNoEnum
    是
    否
End Enum

''' <summary>
''' 安装专业类别枚举
''' </summary>
''' <remarks></remarks>
Public Enum AzSpecialityCategory As Byte
    电气工程 = 1
    水暖工程 = 2
    通风空调 = 3
End Enum

''' <summary>
''' 安装全部专业类型枚举
''' </summary>
''' <remarks></remarks>
Public Enum AzSpecialityType
    '电气工程
    强电 = 1
    弱电 = 2
    '水暖工程
    采暖 = 3
    燃气 = 4
    消防 = 5
    给排水 = 6
    '通风空调
    空调风 = 7
    空调水 = 8
End Enum

''' <summary>
''' 安装全部系统类型ID枚举
''' </summary>
''' <remarks></remarks>
Public Enum AzSystemType
    '电气工程
    '强电
    动力 = 1 'DL
    备用 = 2 'BY
    变配电 = 3 'AH
    照明插座 = 4 'ZC
    防雷接地 = 5 'FLJD
    '弱电
    电话 = 6 'TP
    电视 = 7 'TV
    网络 = 8 'TD
    监控 = 9 'JK
    门禁 = 10 'MJ
    广播 = 11 'GB
    消防报警 = 12 'XFBJ
    其他 = 13 'QT

    '水暖工程
    '采暖
    采暖蒸汽 = 14 'CNZQ
    采暖回水 = 15 'CNHS
    采暖供水 = 16 'CNGS
    采暖热水回水 = 17 'CNRSHS
    采暖热水供水 = 18 'CNRSGS
    热力一次回水管 = 19 'RLYCHS
    热力一次供水管 = 20 'RLYCGS
    '燃气
    燃气管 = 21 'RQ
    '消防
    消防 = 22 'XHL
    喷淋 = 23 'PL
    '给排水
    给水 = 24 'JL
    废水 = 25 'FL
    污水 = 26 'WL
    循环 = 27 'XH
    雨水 = 28 'YL
    泵房 = 29 'BF
    冷凝水 = 30 'NL

    '通风空调
    '空调风
    送风 = 31 'SF
    新风 = 32 'XF
    回风 = 33 'HF
    排风 = 34 'PF
    排烟 = 35 'P
    风烟 = 36 'FY
    除尘 = 37 'CC
    加压 = 38 'JY
    净化 = 39 'JH
    循环风 = 40 'XHF
    送补风 = 41 'SB
    厨房排烟 = 42 'CFP
    消防排烟 = 43 'XFP
    排风排烟 = 44 'PFP
    加压送风 = 45 'JYS
    人防排风 = 46 'RFP
    人防送风 = 47 'RFS
    事故通风 = 48 'TF
    '空调水
    补水 = 49 'BS
    蒸汽 = 50 'ZQ
    自来水 = 51 'ZL
    软化水 = 52 'RH
    空调冷凝水 = 53 'LN
    空调冷媒水 = 54 'LM
    空调冷却水 = 55 'LQ
End Enum

''' <summary>
''' 电气工程专业枚举
''' </summary>
Public Enum ElectricalSpeciality
    强电 = 1
    弱电 = 2
End Enum

''' <summary>
''' 水暖工程专业枚举
''' </summary>
Public Enum WaterSpeciality
    采暖 = 3
    燃气 = 4
    消防 = 5
    给排水 = 6
End Enum

''' <summary>
''' 通风空调专业枚举
''' </summary>
Public Enum HVACSpeciality
    空调风 = 7
    空调水 = 8
End Enum

''' <summary>
''' 电气工程系统枚举
''' </summary>
''' <remarks></remarks>
Public Enum ElectricalSystem
    '强电
    动力 = 1 'DL
    备用 = 2 'BY
    变配电 = 3 'AH
    照明插座 = 4 'ZC
    防雷接地 = 5 'FLJD
    '弱电
    电话 = 6 'TP
    电视 = 7 'TV
    网络 = 8 'TD
    监控 = 9 'JK
    门禁 = 10 'MJ
    广播 = 11 'GB
    消防报警 = 12 'XFBJ
    其他 = 13 'QT
End Enum

''' <summary>
''' 水暖工程系统枚举
''' </summary>
''' <remarks></remarks>
Public Enum WaterSystem
    '采暖
    采暖蒸汽 = 14 'CNZQ
    采暖回水 = 15 'CNHS
    采暖供水 = 16 'CNGS
    采暖热水回水 = 17 'CNRSHS
    采暖热水供水 = 18 'CNRSGS
    热力一次回水管 = 19 'RLYCHS
    热力一次供水管 = 20 'RLYCGS
    '燃气
    燃气管 = 21 'RQ
    '消防
    消防 = 22 'XHL
    喷淋 = 23 'PL
    '给排水
    给水 = 24 'JL
    废水 = 25 'FL
    污水 = 26 'WL
    循环 = 27 'XH
    雨水 = 28 'YL
    泵房 = 29 'BF
    冷凝水 = 30 'NL
End Enum

''' <summary>
''' 通风空调工程系统枚举
''' </summary>
''' <remarks></remarks>
Public Enum HVACSystem
    '空调风
    送风 = 31 'SF
    新风 = 32 'XF
    回风 = 33 'HF
    排风 = 34 'PF
    排烟 = 35 'P
    风烟 = 36 'FY
    除尘 = 37 'CC
    加压 = 38 'JY
    净化 = 39 'JH
    循环风 = 40 'XHF
    送补风 = 41 'SB
    厨房排烟 = 42 'CFP
    消防排烟 = 43 'XFP
    排风排烟 = 44 'PFP
    加压送风 = 45 'JYS
    人防排风 = 46 'RFP
    人防送风 = 47 'RFS
    事故通风 = 48 'TF
    '空调水
    补水 = 49 'BS
    蒸汽 = 50 'ZQ
    自来水 = 51 'ZL
    软化水 = 52 'RH
    空调冷凝水 = 53 'LN
    空调冷媒水 = 54 'LM
    空调冷却水 = 55 'LQ
End Enum