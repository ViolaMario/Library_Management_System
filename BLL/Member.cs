using System;
using System.Collections.Generic;

namespace LibraryManager.BLL
{
    public class Member
     {
         public String id { get; set; }
         public String name { get; set; }
         public string birthday { get; set; }
         public string nationality { get; set; }
        //public Person nationality { get; set; }
        //public Person birthday { get; set; }

        List<PaymentRequest> paymentRequests;

         public Member(string id, String name, string birthday,string nationality)
         {
             this.id = id;
             this.name = name;
            this.birthday = birthday;
            this.nationality = nationality;
           


            paymentRequests = new List<PaymentRequest>();
         }



         public void FulfilPaymentRequest(PaymentRequest request)
         {
             if (paymentRequests.Contains(request))
             {
                 _ = paymentRequests.Remove(request);
             }
         }

         public bool HasOngoingPaymentRequest()
         {
             return (paymentRequests.Count > 0);
         }

         public void AssignPaymentRequest(PaymentRequest request)
         {
             paymentRequests.Add(request);
         }
     }
    

}
