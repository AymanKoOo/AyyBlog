using Core.Entites.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Core.Entites
{
    public class Post
    {
        public int id { get; set; }
        public string Slug { get; set; }

        //Foreign Key//
        public string authorID { get; set; }
        public ApplicationUser applicationUser { get; set; }
        public string picture { get; set; }

        public bool trending { get; set; }

        public string title { get; set; }

        public string summary { get; set; }
        
        public string content { get; set; }

        public DateTime createdAt { get; set; }

        public DateTime updatedAt { get; set; }

        public ICollection<post_tag> post_Tags { get; set; }

        public int CategoryId { get; set; }
        public category Category { get; set; }

        public static string CreateSlug(string title)
        {

            title = title?.ToLowerInvariant().Replace(
                " ", "-", StringComparison.OrdinalIgnoreCase) ?? string.Empty;
            title = RemoveDiacritics(title);
            title = RemoveReservedUrlCharacters(title);

            return title.ToLowerInvariant();
        }
        private static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        private static string RemoveReservedUrlCharacters(string text)
        {
            var reservedCharacters = new List<string> { "!", "#", "$", "&", "'", "(", ")", "*", ",", "/", ":", ";", "=", "?", "@", "[", "]", "\"", "%", ".", "<", ">", "\\", "^", "_", "'", "{", "}", "|", "~", "`", "+" };

            foreach (var chr in reservedCharacters)
            {
                text = text.Replace(chr, string.Empty, StringComparison.OrdinalIgnoreCase);
            }

            return text;
        }
    }
}
