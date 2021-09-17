using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Domain
{
    public class SendVoteRequest : IRequest<SendVotesResponse>
    {
        public int VoterId { get; set; }

        public int BeerId { get; set; }
    }
}
