using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Dtos.Stock;
using server.Models;

namespace server.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                comments = stockModel.Comments.Select(x => x.ToCommentDto()).ToList()
            };

        }

        public static Stock ToStockFromCreateDTO(this CreateStockRequestDto stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap
            };
        }
        public static Stock ToStockFromFMB(this FMBStock fmbStock)
        {
            return new Stock
            {
                Symbol = fmbStock.symbol,
                CompanyName = fmbStock.companyName,
                Purchase = (decimal)fmbStock.price,
                LastDiv = (decimal)fmbStock.lastDiv,
                Industry = fmbStock.industry,
                MarketCap = fmbStock.mktCap
            };
        }
    }
}