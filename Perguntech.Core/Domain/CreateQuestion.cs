namespace Perguntech.Core.Domain
{
    public class CreateQuestionDomain
    {
        public QuestionDomain Question { get; set; }
        public List<string> CategoryNames { get; set; }
    }

}