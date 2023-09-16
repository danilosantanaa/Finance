using Finance.Domain;

namespace Finance.Persistence.AutomaticRegistration
{
    public static class AutomaticRegistration
    {
        public static Estado[] SaveLocalidadePadrao()
        {

            return new Estado[] {
                new Estado {
                    Id = 1,
                    Nome = "Pernambuco",
                    Uf = "PE",
                    Cidades = new Cidade [] {
                        new Cidade { Id = 1, Nome = "Petrolina", EstadoId = 1 },
                        new Cidade { Id = 2, Nome = "Lagoa Grande", EstadoId = 1 }
                    }
                },

                new Estado {
                    Id = 2,
                    Nome = "São Paulo",
                    Uf = "SP",
                    Cidades = new Cidade [] {
                        new Cidade { Id = 3, Nome = "São Paulo", EstadoId = 2 }
                    }
                }
            };
        }
    }
}