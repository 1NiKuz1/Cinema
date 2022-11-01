using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema
{
    internal class ActionsWithPictures
    {
        public static string pathImages = @"D:\project-code\_Sharaga\Kuznetcov_N_BASE\_Cursach\Cinema\Cinema\Pictures\";

        public static byte[] ConsertImageToBase64(string iFile)
        {
            iFile += ".jpg";
            FileInfo fInfo = new FileInfo(pathImages + iFile);
            long numBytes = fInfo.Length;
            FileStream fStream = new FileStream(pathImages + iFile, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fStream);
            // конвертация изображения в байты
            byte[] imageData = br.ReadBytes((int)numBytes);
            return imageData;
        }
        public static void PutImageBase64InDb(string iFile, int id)
        {
            FileInfo fInfo = new FileInfo(pathImages + iFile);
            long numBytes = fInfo.Length;
            FileStream fStream = new FileStream(pathImages + iFile, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fStream);
            // конвертация изображения в байты
            byte[] imageData = br.ReadBytes((int)numBytes);
            // получение расширения файла изображения не забыв удалить точку перед расширением
            // запись изображения в БД
            using (SqlConnection sqlConnection = new SqlConnection(@"data source=NIKUZ;initial catalog=cinema;integrated security=True")) // строка подключения к БД
            {
                string commandText = "UPDATE Movie SET screen = @screen WHERE idMovie = @id"; // запрос на вставку
                SqlCommand command = new SqlCommand(commandText, sqlConnection);
                command.Parameters.AddWithValue("@screen", (object)imageData); // записываем само изображение
                command.Parameters.AddWithValue("@id", id); // записываем id
                sqlConnection.Open();
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        public static void GetBase64ImageFromDb(int id)
        {
            //if (File.Exists($"{pathImages}movie{id}.jpg")) return;
            List<byte[]> iScreen = new List<byte[]>(); // сделав запрос к БД мы получим множество строк в ответе, поэтому мы их сможем загнать в массив/List
            using (SqlConnection sqlConnection = new SqlConnection(@"data source=NIKUZ;initial catalog=cinema;integrated security=True"))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = $"SELECT screen FROM Movie WHERE idMovie = {id}"; // наша запись в БД под id=1, поэтому в запросе "WHERE [id] = 1"
                SqlDataReader sqlReader = sqlCommand.ExecuteReader();
                byte[] iTrimByte = null;
                while (sqlReader.Read()) // считываем и вносим в лист результаты
                {
                    iTrimByte = (byte[])sqlReader["screen"]; // читаем строки с изображениями
                    iScreen.Add(iTrimByte);
                }
                sqlConnection.Close();
            }
            // конвертируем бинарные данные в изображение
            byte[] imageData = iScreen[0]; // возвращает массив байт из БД. Так как у нас SQL вернёт одну запись и в ней хранится нужное нам изображение, то из листа берём единственное значение с индексом '0'
            MemoryStream ms = new MemoryStream(imageData);
            System.Drawing.Image newImage = System.Drawing.Image.FromStream(ms);
            // сохраняем изоражение на диск
            string imageName = @"" + pathImages + "movie_" + id + ".jpg";
            newImage.Save(imageName, ImageFormat.Jpeg);
        }
    }
}
