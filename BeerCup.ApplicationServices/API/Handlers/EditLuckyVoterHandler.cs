using AutoMapper;
using BeerCup.ApplicationServices.API.Domain;
using BeerCup.ApplicationServices.API.Domain.Models;
using BeerCup.ApplicationServices.API.ErrorHandling;
using BeerCup.DataAccess;
using BeerCup.DataAccess.CQRS.Commands;
using BeerCup.DataAccess.CQRS.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Handlers
{
    public class EditLuckyVoterHandler : IRequestHandler<EditLuckyVoterRequest, EditLuckyVoterResponse>
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly IQueryExecutor _queryExecutor;
        private readonly IMapper _mapper;

        public EditLuckyVoterHandler(ICommandExecutor commandExecutor, IQueryExecutor queryExecutor, IMapper mapper)
        {
            _commandExecutor = commandExecutor;
            _queryExecutor = queryExecutor;
            _mapper = mapper;
        }

        public async Task<EditLuckyVoterResponse> Handle(EditLuckyVoterRequest request, CancellationToken cancellationToken)
        {
            var query = new GetLuckyVoterQuery
            {
                BattleId = request.BattleId
            };

            var luckyVoterToUpdate = await _queryExecutor.Execute(query);
            if (luckyVoterToUpdate == null)
            {
                return new EditLuckyVoterResponse
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            luckyVoterToUpdate.UserId = request.VoterId;

            var command = new EditLuckyVoterCommand
            {
                Parameter = luckyVoterToUpdate
            };

            var luckyVoterFromDb = await _commandExecutor.Execute(command);
            if (luckyVoterFromDb == null)
            {
                return new EditLuckyVoterResponse
                {
                    Error = new ErrorModel(ErrorType.InternalServerError)
                };
            }

            return new EditLuckyVoterResponse
            {
                Data = _mapper.Map<LuckyVoter>(luckyVoterFromDb)
            };
        }
    }
}
