namespace Biashara_POS.DTOs.AppFunction
{
    public class AppFunctionDto
    {
        public int AppFunctionId { get; set; }

        public string FunctionName { get; set; } = "";

        public int ModuleId { get; set; }

        public string ModuleName { get; set; } = "";
    }
}