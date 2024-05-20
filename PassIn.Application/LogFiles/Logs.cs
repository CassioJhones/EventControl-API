namespace PassIn.Application.LogFiles;

public static class Log
{

    public static void LogToFile(string titulo, string mensagem)
    {
        string arquivo = DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
        StreamWriter swLog = File.Exists(arquivo) ? File.AppendText(arquivo) : new StreamWriter(arquivo);
        swLog.WriteLine($"Log: {GeradorId()}");
        swLog.WriteLine(DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString());
        swLog.WriteLine($"Titulo: {titulo}");
        swLog.WriteLine($"Mensagem: {mensagem}");
        swLog.WriteLine($"------------------------------------------\n");
        swLog.Close();
    }

    private static string GeradorId()
    {
        Guid id = Guid.NewGuid();
        string senha = id.ToString();
        string letras = new(senha.Where(char.IsLetter).ToArray());
        return letras;
    }
}