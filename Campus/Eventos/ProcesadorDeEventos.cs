namespace Campus.Eventos
{
    public class ProcesadorDeEventos : IProcessadorDeEventos
    {
        public void ProcesarEvento(string tipo)
        {
            var tipo = DeterminarEvento(tipo);
        }
    }
    enum TipoDeEventos{
        estudiante_publicado,
        desconocido
    }
}
