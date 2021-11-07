using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Domain
{
    public class AddNewBattleRequest : IRequest<AddNewBattleResponse>
    {
        public int BattleNo { get; set; }
        public string BattleName { get; set; }
        public string Style { get; set; }
        public string PubName { get; set; }
        public DateTime? Date { get; set; }
    }
}
