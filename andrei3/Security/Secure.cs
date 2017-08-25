using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using andrei3.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace andrei3.Security
{
    public class Secure
    {
        private ApplicationUserManager _userManager;
        static private string key1 = "r/Alx6XVVQDthORazNX1YPY8pjdlbHIhp7WXbQ6A5m0=";
        static private string key2 = "cHbI610XA6OqyFVy/qDq3A==";
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public async Task<bool> LoginIn(string email, string password)
        {

            ApplicationSignInManager _signInManager = null;
            var manger = _userManager ?? System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            var result = await signInManager.PasswordSignInAsync(email, password, true, shouldLockout: false);
            if (result.ToString() == "Success")
            {
                try
                {
                    HttpContext.Current.Response.AppendHeader("AuthorizationAndrei", "Secure with header");

                    string UserData = email + ":" + password;

                    using (Aes myAes = Aes.Create())
                    {
                        byte[] keyEncrypt = Convert.FromBase64String(key1);
                        byte[] keyEncrypt1 = Convert.FromBase64String(key2);
                        byte[] encrypted = EncryptStringToBytes_Aes(UserData, keyEncrypt, keyEncrypt1);

                        string data = Convert.ToBase64String(encrypted);
                        HttpContext.Current.Response.Headers.Add("Authorization", data);
                    }
                    return true;

                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return false;
        }

        public async Task<bool> LoginInDinamicKey(string email, string password)
        {
            ApplicationSignInManager _signInManager = null;
            var manger = _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            var result = await signInManager.PasswordSignInAsync(email, password, true, shouldLockout: false);
            if (result.ToString() == "Success")
            {
                try
                {
                    var user = _context.Users.FirstOrDefault(u => u.Email == email);
                    using (Aes myAes = Aes.Create())
                    {
                        string UserData =  password;

                        byte[] encrypted = EncryptStringToBytes_Aes(UserData, myAes.Key, myAes.IV);
                        string data = Convert.ToBase64String(encrypted);

                        string keySave1 = Convert.ToBase64String(myAes.Key);
                        string keySave2 = Convert.ToBase64String(myAes.IV);

                        user.key1 = keySave1;
                        user.key2 = keySave2;

                        _context.Entry(user).State = EntityState.Modified;
                        _context.SaveChanges();
                        byte[] keyEncrypt = Convert.FromBase64String(key1);
                        byte[] keyEncrypt1 = Convert.FromBase64String(key2);
                        byte[] EncryptedEmail = EncryptStringToBytes_Aes(email, keyEncrypt, keyEncrypt1);

                        string Base64Email = Convert.ToBase64String(EncryptedEmail);
                        HttpContext.Current.Response.Headers.Add("Key", Base64Email);
                        HttpContext.Current.Response.Headers.Add("Authorization", data);
                    }
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }

            }

            return false;
        }

        public async Task<bool> checkUserDinamicKey(string authorization1,string authorization2)
        {
            if (authorization1 == "")
                return false;
            using (Aes myAes = Aes.Create())
            {
                try
                {
                    myAes.Padding = PaddingMode.None;
                    byte[] recoveredKey = Convert.FromBase64String(authorization1);
                    byte[] keyEncrypt = Convert.FromBase64String(key1);
                    byte[] keyEncrypt1 = Convert.FromBase64String(key2);

                   string UserEmail = DecryptStringFromBytes_Aes(recoveredKey, keyEncrypt, keyEncrypt1);

                    var user = _context.Users.FirstOrDefault(u => u.Email == "leonteiandrei@gmail.com");

                    byte[] pass = Convert.FromBase64String(authorization2);
                    byte[] keyPassEncrypt = Convert.FromBase64String(user.key1);
                    byte[] keyPasssEncrypt1 = Convert.FromBase64String(user.key2);
                    string password = DecryptStringFromBytes_Aes(pass, keyPassEncrypt, keyPasssEncrypt1);
               
                   
                   


                    bool check =await LoginInDinamicKey(UserEmail, password);     
                    return check ? true : false;


                    }
                catch (Exception e)
                {

                }

            }


            return false;

        }
  
        public bool checkUser(string authorization)
        {
            if (authorization == "")
                return false;

            using (Aes myAes = Aes.Create())
            {
                try
                {
                    byte[] recoveredKey = Convert.FromBase64String(authorization);
                    byte[] keyEncrypt = Convert.FromBase64String(key1);
                    byte[] keyEncrypt1 = Convert.FromBase64String(key2);
                    string roundtrip = DecryptStringFromBytes_Aes(recoveredKey, keyEncrypt, keyEncrypt1);
                    string[] Userdata = roundtrip.Split(':');
                    string email = "";
                    email = Userdata[0];

                    var check = _context.Users.First(m => m.Email == email);
                    HttpContext.Current.Response.Headers.Add("Authorization", roundtrip);
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }

            }
        }

        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }

        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key
, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }
    }
}