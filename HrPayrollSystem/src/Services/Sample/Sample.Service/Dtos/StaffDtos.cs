
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Service.Dtos
{

    public record StaffCreateModel(string Name, List<AddressModel> Addresses);

    public record AddressModel(string Street, string City);
}
