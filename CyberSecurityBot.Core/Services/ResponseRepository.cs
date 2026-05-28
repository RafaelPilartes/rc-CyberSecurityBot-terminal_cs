using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CyberSecurityBot.Core.Models;
using Newtonsoft.Json;

namespace CyberSecurityBot.Core.Services
{
    public class ResponseRepository
    {
        private readonly List<Response> _responses;

        public ResponseRepository(string jsonFilePath)
        {
            if (!File.Exists(jsonFilePath))
                throw new FileNotFoundException("Responses file not found.", jsonFilePath);

            string json = File.ReadAllText(jsonFilePath);
            var file = JsonConvert.DeserializeObject<ResponsesFile>(json);
            _responses = file?.responses ?? new List<Response>();
        }

        public IReadOnlyList<Response> All => _responses;

        public IReadOnlyList<string> GetTopicKeys()
            => _responses
                .Where(r => !string.IsNullOrWhiteSpace(r.topicKey))
                .Select(r => r.topicKey)
                .Distinct()
                .ToList();

        public Response FindByTopicKey(string topicKey)
            => _responses.FirstOrDefault(r => string.Equals(r.topicKey, topicKey, StringComparison.OrdinalIgnoreCase));
    }
}
