Namespace Layer

    ''' <summary>
    ''' 线型名称枚举
    ''' </summary>
    Public Enum LineType
        ''' <summary>
        ''' 随图层
        ''' </summary>
        ByLayer
        ''' <summary>
        ''' 随块
        ''' </summary>
        ByBlock
        ''' <summary>
        ''' 连续实线
        ''' </summary>
        Continuous
        ''' <summary>
        ''' 划线
        ''' </summary>
        ACAD_ISO02W100
        ''' <summary>
        ''' 划 空格线
        ''' </summary>
        ACAD_ISO03W100
        ''' <summary>
        ''' 长划、点线
        ''' </summary>
        ACAD_ISO04W100
        ''' <summary>
        ''' 长划、双点线
        ''' </summary>
        ACAD_ISO05W100
        ''' <summary>
        ''' 长划、三点线
        ''' </summary>
        ACAD_ISO06W100
        ''' <summary>
        ''' 点线
        ''' </summary>
        ACAD_ISO07W100
        ''' <summary>
        ''' 长划、短划线
        ''' </summary>
        ACAD_ISO08W100
        ''' <summary>
        ''' 长划、双短划线
        ''' </summary>
        ACAD_ISO09W100
        ''' <summary>
        ''' 划、点线
        ''' </summary>
        ACAD_ISO10W100
        ''' <summary>
        ''' 双划、点线
        ''' </summary>
        ACAD_ISO11W100
        ''' <summary>
        ''' 划、双点线
        ''' </summary>
        ACAD_ISO12W100
        ''' <summary>
        ''' 双划、双点线
        ''' </summary>
        ACAD_ISO13W100
        ''' <summary>
        ''' 划、三点线
        ''' </summary>
        ACAD_ISO14W100
        ''' <summary>
        ''' 双划、三点线
        ''' </summary>
        ACAD_ISO15W100
        ''' <summary>
        ''' 虚线
        ''' </summary>
        DASH
        ''' <summary>
        ''' 点、划线，类似于建筑轴线
        ''' </summary>
        DASHHOT
    End Enum

    ''' <summary>
    ''' DXF文件常用组码数值枚举
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum DxfCode
        ''' <summary>
        ''' 实体类型的字符串（固定）
        ''' </summary>
        EntityType = 0
        ''' <summary>
        ''' 名称（属性标记、块名等）
        ''' </summary>
        Name = 2
        ''' <summary>
        ''' 线型名（固定）
        ''' </summary>
        LineTypeName = 6
        ''' <summary>
        ''' 文字样式名（固定）
        ''' </summary>
        TextStyleName = 7
        ''' <summary>
        ''' 图层名（固定）
        ''' </summary>
        LayerName = 8
        ''' <summary>
        ''' 颜色号（固定）
        ''' </summary>
        ColorCode = 62
        ''' <summary>
        ''' 实体对象线型名组码
        ''' </summary>
        LineType = 6
        ''' <summary>
        ''' 通用实体
        ''' </summary>
        CommonEntity = 5020
        ''' <summary>
        ''' 字符型扩展数据组码
        ''' </summary>
        XDataString = 1000
        ''' <summary>
        ''' 双精度扩展数据组码
        ''' </summary>
        XDataDounble = 1040
        ''' <summary>
        ''' 整型扩展数据组码
        ''' </summary>
        XDataLong = 1071
    End Enum

    ''' <summary>
    ''' DXF文件基础图元类型名称枚举
    ''' </summary>
    Public Enum EntityDxfTypeName As Short
        ''' <summary>
        ''' 线段
        ''' </summary>
        LINE = 1
        ''' <summary>
        ''' 多段线，包括多边形
        ''' </summary>
        LWPOLYLINE = 2
        ''' <summary>
        ''' 圆，不包括圆弧
        ''' </summary>
        CIRCLE = 3
        ''' <summary>
        ''' 圆弧
        ''' </summary>
        ARC = 4
        ''' <summary>
        ''' 样条曲线
        ''' </summary>
        SPLINE = 5
        ''' <summary>
        ''' 椭圆，包括椭圆弧
        ''' </summary>
        ELLIPSE = 6
        ''' <summary>
        ''' 块参考
        ''' </summary>
        INSERT = 7
        ''' <summary>
        ''' 单行文本
        ''' </summary>
        TEXT = 8
        ''' <summary>
        ''' 多行文本
        ''' </summary>
        MTEXT = 9

        NONE
    End Enum

    ''' <summary>
    ''' CAD基础图元对象类型名称枚举
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EntityCadTypeName As Short
        ''' <summary>
        ''' 线段
        ''' </summary>
        McDbLine = 1
        ''' <summary>
        ''' 多段线
        ''' </summary>
        McDbPolyline = 2
        ''' <summary>
        ''' 圆对象
        ''' </summary>
        McDbCircle = 3
        ''' <summary>
        ''' 圆弧
        ''' </summary>
        McDbArc = 4
        ''' <summary>
        ''' 椭圆，包括椭圆弧
        ''' </summary>
        McDbEllipse = 5
        ''' <summary>
        ''' 样条曲线
        ''' </summary>
        McDbSpline = 6
        ''' <summary>
        ''' 快参考
        ''' </summary>
        McDbBlockReference = 7
        ''' <summary>
        ''' 单行文字
        ''' </summary>
        McDbText = 8
        ''' <summary>
        ''' 多行文字
        ''' </summary>
        McDbMText = 9
    End Enum

End Namespace
