using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPro.Domain.Entities;

namespace HPro.Domain.Interfaces
{
    public interface IHCore_Tag_Repositories
    {
        List<HCore_Tag> HCore_Tags { get; set; }

    }
}
