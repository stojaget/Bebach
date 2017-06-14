using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BebachModel
{
    [MetadataType(typeof(BebaMetadata))]
    public partial class Beba
    {
    }

    [MetadataType(typeof(AktivnostMetadata))]
    public partial class Aktivnost
    {
    }

}
