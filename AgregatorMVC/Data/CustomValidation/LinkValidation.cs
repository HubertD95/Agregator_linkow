using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using AgregatorMVC.Models;
using System.Net;

namespace AgregatorMVC.Data.CustomValidation
{
    public class LinkValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var link = (LinkModel)validationContext.ObjectInstance;

            if (link.Link != null && link.Link != "")
            {
                if (!link.Link.Contains("http"))
                {
                    link.Link = "https://" + link.Link;
                }

                try
                {
                    HttpWebRequest request = WebRequest.Create(link.Link) as HttpWebRequest;
                    request.Method = "HEAD";
                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                    response.Close();

                    return ValidationResult.Success;
                }
                catch
                {
                    return new ValidationResult("Brak odpowiedzi z podanej strony");
                }
            }
            else 
                return new ValidationResult("Link jest niepoprawny");
        }  
    }
}
