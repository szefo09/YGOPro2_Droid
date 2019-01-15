using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

public class HttpDldFile
{
    public bool Download(string url, string filename)
    {
        bool flag = false;
        long startPosition = 0;
        FileStream writeStream;

        if (File.Exists(filename + ".tmp"))
        {
            writeStream = File.OpenWrite(filename + ".tmp");
            startPosition = writeStream.Length;
            writeStream.Seek(startPosition, SeekOrigin.Current);
        }
        else
        {
            writeStream = new FileStream(filename + ".tmp", FileMode.Create);
            startPosition = 0;
        }
        try
        {
            HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            if (startPosition > 0)
            {
                myRequest.AddRange((int)startPosition);
            }
            Stream readStream = myRequest.GetResponse().GetResponseStream();
            byte[] btArray = new byte[2048];
            int contentSize = readStream.Read(btArray, 0, btArray.Length);
            while (contentSize > 0)
            {
                writeStream.Write(btArray, 0, contentSize);
                contentSize = readStream.Read(btArray, 0, btArray.Length);
            }

            writeStream.Close();
            readStream.Close();
            flag = true;

            if(File.Exists(filename))
            {
                File.Delete(filename);
            }
            File.Move(filename + ".tmp", filename);
        }
        catch (Exception)
        {
            writeStream.Close();
            flag = false;
        }
        return flag;
    }

}