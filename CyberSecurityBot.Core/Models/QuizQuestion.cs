using System.Collections.Generic;

namespace CyberSecurityBot.Core.Models
{
    /// <summary>
    /// A single quiz question (Part 3 Task 2). Supports both multiple-choice
    /// (four options) and true/false (two options) by varying the option count.
    /// </summary>
    public class QuizQuestion
    {
        public string Text { get; set; }
        public List<string> Options { get; set; } = new List<string>();

        /// <summary>Zero-based index of the correct option.</summary>
        public int CorrectIndex { get; set; }

        public string Explanation { get; set; }

        public QuizQuestion() { }

        public QuizQuestion(string text, List<string> options, int correctIndex, string explanation)
        {
            Text = text;
            Options = options;
            CorrectIndex = correctIndex;
            Explanation = explanation;
        }
    }
}
