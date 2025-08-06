namespace Learning.Common.Constants
{
    public class SecurityConstant
    {
        public const string NAME_PATTERN = @"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s'-]+$";
        public const string DOCUMENT_NUMBER_PATTERN = @"^\d+$";
        public const string USERNAME_PATTERN = @"^[a-zA-Z0-9._-]{3,30}$";
        public const string PASSWORD_PATTERN = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$";


        //length limits

        public const int NAME_MAX_LENGTH = 20;
        public const int USERNAME_MAX_LENGTH = 20;
        public const int EMAIL_MAX_LENGTH = 50;
        public const int DOCUMENT_NUMBER_MAX_LENGTH = 20;
        public const int ADRESS_MAX_LENGTH = 50;

        //password limits
        public const int PASSWORD_MIN_LENGTH = 8;
        public const int PASSWORD_MAX_LENGTH = 20;
        
        //age limits
        public const int AGE_MIN = 12;
        public const int AGE_MAX = 100;

        //allowed file extensions

        public static readonly string[] ALLOWED_FILE_EXTENSIONS = { ".jpg", ".jpeg", ".png", ".gif" };
        public static readonly string[] ALLOWED_DOCUMENT_EXTENSIONS = { ".pdf", ".doc", ".docx", ".xls", ".ppt", ".txt" };
        public static readonly string[] ALLOWED_VIDEO_EXTENSIONS = { ".mp4", ".avi", ".mov", ".mkv" };
        public const int MAX_FILE_SIZE_MB = 5;

        //Types and enums

        public static readonly string[] ALLOWED_COMMETABLE_TYPES = { "Lesson", "Task", "Course" };
        public static readonly string[] ALLOWED_ATTACHABLE_TYPES = { "Lesson", "Task", "Course" };
        public static readonly string[] ALLOWED_STATUS_VALUES = { "activo", "inactivo", "pendiente", "en_progreso", "finalizado", "cancelado" };

        public static readonly string[] ALLOWED_ROLES = { "Estudiante", "Instructor", "Administrador" };  
        public static readonly string[] ALLOWED_CONVERSATION_TYPES = { "Privada", "Grupal", "Soporte", "Tutoría" };  
        public static readonly string[] ALLOWED_PARTICIPANT_ROLES = { "Instructor", "Estudiante", "Invitado", "Administrador" };

        //Dangerous characters and patterns
        public static readonly string[] SQL_INJECTION_PATTERNS = { "'", "\"", ";", "--", "/*", "*/", "xp_", "sp_", "DROP", "DELETE", "INSERT", "UPDATE", "SELECT"};
        public static readonly string[] XSS_PATTERNS = { "<script", "</script>", "javascript:", "vbscript:", "onload=", "onerror=", "onclick=", "onmouseover="};
        
        //UtilitY
        public static readonly string[] URL_PROTOCOLS_ALLOWED = { "http://", "https://" };



    }
}
