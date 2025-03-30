using System;
using System.Security.Cryptography;
using System.Text;

class Program
{
   public  static string storedHash = "";

    public static void Main(string[] args)
    {
        const string Menu = "Menu \n1. Registre \n2. Verificació de dades\n3. Encriptació i desencriptació amb RSA\n4. Sortir"; 

        while (true)
        {
            
            Console.WriteLine(Menu);
            Console.Write("Escull una opció: ");
            string opcion = Console.ReadLine();

            if (opcion == "1")
            {
                Registrar();
            }
            else if (opcion == "2")
            {
                Verificar();
            }
            else if (opcion == "3")
            {
                EncriptarDesencriptarRSA();
            }
            else if (opcion == "4")
            {
                break;
            }
            else
            {
                Console.WriteLine("Opció no vàlida.");
            }
        }
    }

    static void Registrar()
    {
        Console.Write("Introdueix el username: ");
        string username = Console.ReadLine().Trim(); 
        Console.Write("Introdueix la password: ");
        string password = Console.ReadLine().Trim(); 

        string combinacion = username + password;
        storedHash = CalcularHash(combinacion); 
        Console.WriteLine("Hash guardat: " + storedHash);
    }

    static void Verificar()
    {
        if (string.IsNullOrEmpty(storedHash))
        {
            Console.WriteLine("Primer has de registrar un usuari.");
            return;
        }

        Console.Write("Introdueix el username: ");
        string username = Console.ReadLine().Trim();
        Console.Write("Introdueix la password: ");
        string password = Console.ReadLine().Trim();

        string combinacion = username + password;
        string hashNuevo = CalcularHash(combinacion);

        if (hashNuevo == storedHash)
        {
            Console.WriteLine("Les dades són correctes.");
        }
        else
        {
            Console.WriteLine("Les dades no coincideixen.");
            Console.WriteLine("Hash esperat: " + storedHash); 
            Console.WriteLine("Hash rebut: " + hashNuevo); 
        }
    }

    static string CalcularHash(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }

    static void EncriptarDesencriptarRSA()
    {
        Console.Write("Introdueix un text per encriptar: ");
        string texto = Console.ReadLine();

        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
        {
            byte[] textoBytes = Encoding.UTF8.GetBytes(texto);
            byte[] encriptado = rsa.Encrypt(textoBytes, true);
            string encriptadoTexto = Convert.ToBase64String(encriptado);

            byte[] desencriptado = rsa.Decrypt(encriptado, true);
            string desencriptadoTexto = Encoding.UTF8.GetString(desencriptado);

            Console.WriteLine("Text encriptat: " + encriptadoTexto);
            Console.WriteLine("Text desencriptat: " + desencriptadoTexto);
        }
    }
}