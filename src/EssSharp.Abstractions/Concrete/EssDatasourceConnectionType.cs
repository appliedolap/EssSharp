using System.Runtime.Serialization;

namespace EssSharp
{
    /// <summary>
    /// Defines Type
    /// </summary>
    public enum EssDatasourceConnectionType
    {   
        /// <summary>
        /// Enum UNKNOWN for value: UNKNOWN
        /// </summary>
        UNKNOWN = 0,

        /// <summary>
        /// Enum FILE for value: FILE
        /// </summary>
        FILE = 1,

        /// <summary>
        /// Enum DB for value: DB
        /// </summary>
        DB = 2,

        /// <summary>
        /// Enum ESSBASE for value: ESSBASE
        /// </summary>
        ESSBASE = 3,

        /// <summary>
        /// Enum BI for value: BI
        /// </summary>
        BI = 4

    }

    /// <summary>
    /// Defines EssDatasourceConnectionSubtype
    /// </summary>
    public enum EssDatasourceConnectionSubtype
    {
        /// <summary>
        /// Enum UNKNOWN for value: UNKNOWN
        /// </summary>
        UNKNOWN = 0,

        /// <summary>
        /// Enum TEMPLATE for value: TEMPLATE
        /// </summary>
        TEMPLATE = 1,

        /// <summary>
        /// Enum EXCELFILE for value: EXCELFILE
        /// </summary>
        EXCELFILE = 2,

        /// <summary>
        /// Enum DB for value: DB
        /// </summary>
        DB = 3,

        /// <summary>
        /// Enum DELIMITEDFILE for value: DELIMITEDFILE
        /// </summary>
        DELIMITEDFILE = 4,

        /// <summary>
        /// Enum FIXEDWIDTHFILE for value: FIXEDWIDTHFILE
        /// </summary>
        FIXEDWIDTHFILE = 5,

        /// <summary>
        /// Enum BI for value: BI
        /// </summary>
        BI = 6,

        /// <summary>
        /// Enum ESSBASE for value: ESSBASE
        /// </summary>
        ESSBASE = 7,

        /// <summary>
        /// Enum JDBC for value: JDBC
        /// </summary>
        JDBC = 8,

        /// <summary>
        /// Enum SPARK for value: SPARK
        /// </summary>
        SPARK = 9,

        /// <summary>
        /// Enum MSSQL for value: MS_SQL
        /// </summary>
        MSSQL = 10,

        /// <summary>
        /// Enum MYSQL for value: MYSQL
        /// </summary>
        MYSQL = 11,

        /// <summary>
        /// Enum DB2 for value: DB2
        /// </summary>
        DB2 = 12,

        /// <summary>
        /// Enum ORACLE for value: ORACLE
        /// </summary>
        ORACLE = 13,

        /// <summary>
        /// Enum FILE for value: FILE
        /// </summary>
        FILE = 14

    }
}
