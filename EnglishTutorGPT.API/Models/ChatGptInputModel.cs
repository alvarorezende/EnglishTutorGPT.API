namespace EnglishTutorGPT.API.Models
{
    public class ChatGptInputModel
    {
        public ChatGptInputModel(string prompt)
        {
            this.prompt = $"Correct this english phrase: {prompt}";
            this.temperature = 0.2m;
            this.max_tokens = 100;
            this.model = "text-davinci-003";
        }

        public string model { get; set; }
        public string prompt { get; set; }
        public int max_tokens { get; set; }
        public decimal temperature { get; set; }
    }
}
