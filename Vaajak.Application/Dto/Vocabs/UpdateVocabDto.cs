using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaajak.Domain.Entities;

namespace Vaajak.Application.Dto.Vocabs
{
    public class UpdateVocabDto
    {
        public Guid Id { get; set; }
        public string Vocabulary { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Voice { get; set; } = string.Empty;

    }
}
