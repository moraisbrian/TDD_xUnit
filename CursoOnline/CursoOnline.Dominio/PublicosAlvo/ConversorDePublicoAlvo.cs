using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.PublicosAlvo;
using System;

namespace CursoOnline.Dominio.PublicosAlvo
{
    public class ConversorDePublicoAlvo : IConversorDePublicoAlvo
    {
        public EPublicoAlvo Converter(string publicoAlvo)
        {
            ValidadorDeRegra.Novo()
                .Quando(!Enum.TryParse<EPublicoAlvo>(publicoAlvo, out var publicoAlvoConvertido), 
                    Resource.PublicoAlvoInvalido)
                .DispararExcecaoSeExistir();

            return publicoAlvoConvertido;
        }
    }
}
