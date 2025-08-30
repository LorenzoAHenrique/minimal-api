using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinimalAPI.Dominio.ModelViews
{
    public struct Home
    {
        public string Mensagem { get => "Bem vindo a APi de Veículos - Minimal API"; }

        public string Doc { get => "/swagger"; }
    }

}