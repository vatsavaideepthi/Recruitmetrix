using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class EnumObjects
    {
       public enum regacctype
        {
            single,
            joint,
            group,
            child
        }

       public enum invoicemodes
       {
           inbound,
           outbound
       }

       public enum filetype
       {
           audio,
           video,
           image,
           pdf,
           word,
           excel,
           text
       }

       public enum taskstatus
       {
           running,
           onhold,
           cancelled,
           completed,
           waiting,
           delayed,
           rejected,
           assigned,
           approved
       }
    }
}