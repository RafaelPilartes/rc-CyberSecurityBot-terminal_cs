using System.Collections.Generic;
using CyberSecurityBot.Core.Models;

namespace CyberSecurityBot.Core.Services
{
    /// <summary>
    /// Provides the cybersecurity quiz questions (Part 3 Task 2). Covers phishing,
    /// password safety, safe browsing, social engineering, malware, secure Wi-Fi
    /// and 2FA, mixing multiple-choice and true/false questions.
    /// </summary>
    public class QuizBank
    {
        /// <summary>Returns a fresh copy of all quiz questions.</summary>
        public List<QuizQuestion> GetQuestions()
        {
            return new List<QuizQuestion>
            {
                new QuizQuestion(
                    "You get an unexpected email asking you to 'verify your account' via a link. What's the safest first step?",
                    new List<string>
                    {
                        "Click the link quickly before the account is locked",
                        "Check the sender and go to the official site directly instead of clicking",
                        "Reply with your username and password",
                        "Forward it to all your contacts"
                    },
                    1,
                    "Phishing links imitate real sites. Navigate to the site yourself rather than trusting the link."),

                new QuizQuestion(
                    "Reusing the same strong password across all your accounts is safe, as long as it's long.",
                    new List<string> { "True", "False" },
                    1,
                    "Reuse means one breached site exposes every account. Use a unique password per site."),

                new QuizQuestion(
                    "What does two-factor authentication (2FA) add to a login?",
                    new List<string>
                    {
                        "A second password you must memorise",
                        "A second, different proof of identity, such as a code on your phone",
                        "A faster way to log in",
                        "Nothing useful"
                    },
                    1,
                    "2FA combines something you know (password) with something you have (a device/code)."),

                new QuizQuestion(
                    "Antivirus software guarantees 100% protection against all malware.",
                    new List<string> { "True", "False" },
                    1,
                    "Antivirus helps a lot but isn't perfect. Safe habits and updates matter too."),

                new QuizQuestion(
                    "A website uses 'https://' and shows a padlock. This means:",
                    new List<string>
                    {
                        "The site is guaranteed safe and trustworthy",
                        "The connection is encrypted, but the site could still be malicious",
                        "The site is government-approved",
                        "Your password can never be stolen there"
                    },
                    1,
                    "HTTPS only encrypts the connection. Scam sites can use HTTPS too."),

                new QuizQuestion(
                    "Someone calls claiming to be from IT and urgently asks for your password. You should:",
                    new List<string>
                    {
                        "Give it since they're from IT",
                        "Refuse and verify through an official channel",
                        "Give a slightly wrong password",
                        "Put them on hold and forget about it"
                    },
                    1,
                    "Legitimate IT never needs your password. This is classic social engineering."),

                new QuizQuestion(
                    "It's fine to do online banking on open public Wi-Fi with no extra protection.",
                    new List<string> { "True", "False" },
                    1,
                    "Open Wi-Fi can be snooped. Use a VPN or your mobile data for sensitive tasks."),

                new QuizQuestion(
                    "Which is a common sign of a phishing message?",
                    new List<string>
                    {
                        "Personalised, error-free text from a known colleague",
                        "Urgent threats, generic greetings and suspicious links",
                        "A message you were clearly expecting",
                        "An email with no links and no requests"
                    },
                    1,
                    "Urgency, generic greetings and odd links are red flags for phishing."),

                new QuizQuestion(
                    "What makes a password strong?",
                    new List<string>
                    {
                        "Your pet's name",
                        "A short, common word",
                        "A long, unique passphrase",
                        "123456"
                    },
                    2,
                    "Length and uniqueness beat complexity tricks. A passphrase is easy to remember and hard to crack."),

                new QuizQuestion(
                    "A free program asks for lots of permissions and tries to disable your antivirus. This is likely:",
                    new List<string>
                    {
                        "Normal behaviour",
                        "A sign of malware",
                        "A performance boost",
                        "Required by Windows"
                    },
                    1,
                    "Disabling security tools and over-asking for permissions are hallmarks of malware."),

                new QuizQuestion(
                    "Receiving a 2FA code you never requested can be a sign someone is trying to access your account.",
                    new List<string> { "True", "False" },
                    0,
                    "An unexpected code often means someone has your password and is attempting to log in. Change it."),

                new QuizQuestion(
                    "Installing software updates promptly helps protect you because they often fix security vulnerabilities.",
                    new List<string> { "True", "False" },
                    0,
                    "Updates frequently patch known holes that attackers exploit. Don't delay them."),

                new QuizQuestion(
                    "In security, 'tailgating' means:",
                    new List<string>
                    {
                        "Driving too close to another car",
                        "Following an authorised person through a secure door without badging in",
                        "A type of phishing email",
                        "A method for creating strong passwords"
                    },
                    1,
                    "Tailgating is a physical social-engineering trick to bypass access controls.")
            };
        }
    }
}
