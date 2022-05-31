using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Aluno.Models;

namespace Aluno.Context
{
    public class Contexto : DbContext
    {
        public Contexto() : base("DBcriptografando")
        {
        }

        public DbSet<AlunoModel> Alunos { get; set; }
    }
}