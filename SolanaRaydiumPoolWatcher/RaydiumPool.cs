﻿using Solnet.Wallet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static SolanaRaydiumPoolWatcher.RaydiumPool;

namespace SolanaRaydiumPoolWatcher
{
    public static class RaydiumPool
    {
        public struct ProgramID
        {
            public readonly PublicKey SERUM_MARKET;
            public readonly PublicKey OPENBOOK_MARKET;

            public readonly PublicKey UTIL1216;

            public readonly PublicKey FarmV3;
            public readonly PublicKey FarmV5;
            public readonly PublicKey FarmV6;

            public readonly PublicKey AmmV4;
            public readonly PublicKey AmmStable;

            public readonly PublicKey CLMM;
            public readonly PublicKey Router;

            // Constructor to initialize all fields
            public ProgramID(PublicKey serumMarket, PublicKey openbookMarket, PublicKey util1216, PublicKey farmV3, PublicKey farmV5, PublicKey farmV6, PublicKey ammV4, PublicKey ammStable, PublicKey clmm, PublicKey router)
            {
                SERUM_MARKET = serumMarket;
                OPENBOOK_MARKET = openbookMarket;
                UTIL1216 = util1216;
                FarmV3 = farmV3;
                FarmV5 = farmV5;
                FarmV6 = farmV6;
                AmmV4 = ammV4;
                AmmStable = ammStable;
                CLMM = clmm;
                Router = router;
            }

        }

        public struct TokenInfo
        {
            public readonly string Symbol;
            public readonly string Name;
            public readonly PublicKey Mint;

            public TokenInfo(string symbol, string name, PublicKey mint)
            {
                Symbol = symbol;
                Name = name;
                Mint = mint;
            }
        }

        // The Solana's Mainnet
        public static readonly ProgramID MAINNET_PROGRAM_ID = new ProgramID(
            serumMarket: new PublicKey("9xQeWvG816bUx9EPjHmaT23yvVM2ZWbrrpZb9PusVFin"),
            openbookMarket: new PublicKey("srmqPvymJeFKQ4zGQed1GFppgkRHL9kaELCbyksJtPX"),

            util1216: new PublicKey("CLaimxFqjHzgTJtAGHU47NPhg6qrc5sCnpC4tBLyABQS"),

            farmV3: new PublicKey("EhhTKczWMGQt46ynNeRX1WfeagwwJd7ufHvCDjRxjo5Q"),
            farmV5: new PublicKey("9KEPoZmtHUrBbhWN1v1KWLMkkvwY6WLtAVUCPRtRjP4z"),
            farmV6: new PublicKey("FarmqiPv5eAj3j1GMdMCMUGXqPUvmquZtMy86QH6rzhG"),

            ammV4: new PublicKey("675kPX9MHTjS2zt1qfr1NYHuzeLXfQM9H24wFSUt1Mp8"),
            ammStable: new PublicKey("5quBtoiQqxF9Jv6KYKctB59NT3gtJD2Y65kdnB1Uev3h"),

            clmm: new PublicKey("CAMMCzo5YL8w4VFF8KVHrK22GGUsp5VTaW7grrKgrWqK"),
            router: new PublicKey("routeUGWgWzqBWFcrCfv8tritsqukccJPu3q5GPP3xS")
            );

        // WSOL
        public static readonly TokenInfo WSOL = new TokenInfo(
            symbol: "WSOL",
            name: "Wrapped SOL",
            mint: new PublicKey("So11111111111111111111111111111111111111112")
            );
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RawLiquidityStateV4
    {
        public ulong Status;
        public ulong Nonce;
        public ulong MaxOrder;
        public ulong Depth;
        public ulong BaseDecimal;
        public ulong QuoteDecimal;
        public ulong State;
        public ulong ResetFlag;
        public ulong MinSize;
        public ulong VolMaxCutRatio;
        public ulong AmountWaveRatio;
        public ulong BaseLotSize;
        public ulong QuoteLotSize;
        public ulong MinPriceMultiplier;
        public ulong MaxPriceMultiplier;
        public ulong SystemDecimalValue;
        public ulong MinSeparateNumerator;
        public ulong MinSeparateDenominator;
        
        public ulong TradeFeeNumerator;
        public ulong TradeFeeDenominator;
        public ulong PnlNumerator;
        public ulong PnlDenominator;
        public ulong SwapFeeNumerator;
        public ulong SwapFeeDenominator;
        public ulong BaseNeedTakePnl;
        public ulong QuoteNeedTakePnl;
        public ulong QuoteTotalPnl;
        public ulong BaseTotalPnl;
        public ulong PoolOpenTime;
        public ulong PunishPcAmount;
        public ulong PunishCoinAmount;
        public ulong OrderbookToInitTime;

        //public UInt128 PoolTotalDepositPc;
        //public UInt128 PoolTotalDepositCoin;

        public UInt128 SwapBaseInAmount;
        public UInt128 SwapQuoteOutAmount;
        public ulong SwapBase2QuoteFee;
        public UInt128 SwapQuoteInAmount;
        public UInt128 SwapBaseOutAmount;
        public ulong SwapQuote2BaseFee;

        // AMM Vault
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] BaseVault;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] QuoteVault;

        // Mint
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] BaseMint;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] QuoteMint;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] LpMint;

        // Market
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] OpenOrders;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] MarketId;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] MarketProgramId;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] TargetOrders;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] WithdrawQueue;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] LpVault;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] Owner;

        // true circulating supply without lock up
        public ulong LpReserve;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] 
        public ulong[] Padding;

        public static RawLiquidityStateV4 Decode(byte[] data)
        {
            RawLiquidityStateV4 state = new RawLiquidityStateV4();

            using (var stream = new MemoryStream(data))
            using (var reader = new BinaryReader(stream))
            {
                state.Status                    = reader.ReadUInt64();
                state.Nonce                     = reader.ReadUInt64();
                state.MaxOrder                  = reader.ReadUInt64();
                state.Depth                     = reader.ReadUInt64();
                state.BaseDecimal               = reader.ReadUInt64();
                state.QuoteDecimal              = reader.ReadUInt64();
                state.State                     = reader.ReadUInt64();
                state.ResetFlag                 = reader.ReadUInt64();
                state.MinSize                   = reader.ReadUInt64();
                state.VolMaxCutRatio            = reader.ReadUInt64();
                state.AmountWaveRatio           = reader.ReadUInt64();
                state.BaseLotSize               = reader.ReadUInt64();
                state.QuoteLotSize              = reader.ReadUInt64();
                state.MinPriceMultiplier        = reader.ReadUInt64();
                state.MaxPriceMultiplier        = reader.ReadUInt64();
                state.SystemDecimalValue        = reader.ReadUInt64();
                state.MinSeparateNumerator      = reader.ReadUInt64();
                state.MinSeparateDenominator    = reader.ReadUInt64();
                state.TradeFeeNumerator         = reader.ReadUInt64();
                state.TradeFeeDenominator       = reader.ReadUInt64();
                state.PnlNumerator              = reader.ReadUInt64();
                state.PnlDenominator            = reader.ReadUInt64();
                state.SwapFeeNumerator          = reader.ReadUInt64();
                state.SwapFeeDenominator        = reader.ReadUInt64();
                state.BaseNeedTakePnl           = reader.ReadUInt64();
                state.QuoteNeedTakePnl          = reader.ReadUInt64();
                state.QuoteTotalPnl             = reader.ReadUInt64();
                state.BaseTotalPnl              = reader.ReadUInt64();
                state.PoolOpenTime              = reader.ReadUInt64();
                state.PunishPcAmount            = reader.ReadUInt64();
                state.PunishCoinAmount          = reader.ReadUInt64();
                state.OrderbookToInitTime       = reader.ReadUInt64();

                state.SwapBaseInAmount          = new UInt128(reader.ReadUInt64(), reader.ReadUInt64());
                state.SwapQuoteOutAmount        = new UInt128(reader.ReadUInt64(), reader.ReadUInt64());
                state.SwapBase2QuoteFee         = reader.ReadUInt64();
                state.SwapBaseOutAmount         = new UInt128(reader.ReadUInt64(), reader.ReadUInt64());
                state.SwapBaseOutAmount         = new UInt128(reader.ReadUInt64(), reader.ReadUInt64());
                state.SwapQuote2BaseFee         = reader.ReadUInt64();
                
                state.BaseVault                 = reader.ReadBytes(32);
                state.QuoteVault                = reader.ReadBytes(32);
                state.BaseMint                  = reader.ReadBytes(32);
                state.QuoteMint                 = reader.ReadBytes(32);
                state.LpMint                    = reader.ReadBytes(32);
                state.OpenOrders                = reader.ReadBytes(32);
                state.MarketId                  = reader.ReadBytes(32);
                state.MarketProgramId           = reader.ReadBytes(32);
                state.TargetOrders              = reader.ReadBytes(32);
                state.WithdrawQueue             = reader.ReadBytes(32);
                state.LpVault                   = reader.ReadBytes(32);
                state.Owner                     = reader.ReadBytes(32);

                state.LpReserve                 = reader.ReadUInt64();

                state.Padding = new ulong[3];
                for (int i = 0; i < 3; i++)
                {
                    state.Padding[i]            = reader.ReadUInt64();
                }
            }

            return state;
        }
    }
    
    public struct LiquidityStateV4
    {
        public RawLiquidityStateV4 Raw;
        
        public readonly ulong Status => Raw.Status;
        public readonly ulong Nonce => Raw.Nonce;
        public readonly ulong MaxOrder => Raw.MaxOrder;
        public readonly ulong Depth => Raw.Depth;
        public readonly ulong BaseDecimal => Raw.BaseDecimal;
        public readonly ulong QuoteDecimal => Raw.QuoteDecimal;
        public readonly ulong State => Raw.State;
        public readonly ulong ResetFlag => Raw.ResetFlag;
        public readonly ulong MinSize => Raw.MinSize;
        public readonly ulong VolMaxCutRatio => Raw.VolMaxCutRatio;
        public readonly ulong AmountWaveRatio => Raw.AmountWaveRatio;
        public readonly ulong BaseLotSize => Raw.BaseLotSize;
        public readonly ulong QuoteLotSize => Raw.QuoteLotSize;
        public readonly ulong MinPriceMultiplier => Raw.MinPriceMultiplier;
        public readonly ulong MaxPriceMultiplier => Raw.MaxPriceMultiplier;
        public readonly ulong SystemDecimalValue => Raw.SystemDecimalValue;
        
        public readonly ulong MinSeparate => (Raw.MinSeparateNumerator / Raw.MinSeparateDenominator);
        public readonly ulong MinSeparateNumerator => Raw.MinSeparateNumerator;
        public readonly ulong MinSeparateDenominator => Raw.MinSeparateDenominator;
        
        public readonly ulong TradeFee => (Raw.TradeFeeNumerator / Raw.TradeFeeDenominator);
        public readonly ulong TradeFeeNumerator => Raw.TradeFeeNumerator;
        public readonly ulong TradeFeeDenominator => Raw.TradeFeeDenominator;
        
        public readonly ulong Pnl => (Raw.PnlNumerator / Raw.PnlDenominator);
        public readonly ulong PnlNumerator => Raw.PnlNumerator;
        public readonly ulong PnlDenominator => Raw.PnlDenominator;
        
        public readonly ulong SwapFee => (Raw.SwapFeeNumerator / Raw.SwapFeeDenominator);
        public readonly ulong SwapFeeNumerator => Raw.SwapFeeNumerator;
        public readonly ulong SwapFeeDenominator => Raw.SwapFeeDenominator;
        
        public readonly ulong BaseNeedTakePnl => Raw.BaseNeedTakePnl;
        public readonly ulong QuoteNeedTakePnl => Raw.QuoteNeedTakePnl;
        
        public readonly ulong QuoteTotalPnl => Raw.QuoteTotalPnl;
        public readonly ulong BaseTotalPnl => Raw.BaseTotalPnl;
        public readonly ulong PoolOpenTime => Raw.PoolOpenTime;
        public readonly ulong PunishPcAmount => Raw.PunishPcAmount;
        public readonly ulong PunishCoinAmount => Raw.PunishCoinAmount;
        public readonly ulong OrderbookToInitTime => Raw.OrderbookToInitTime;

        //public readonly UInt128 PoolTotalDepositPc;
        //public readonly UInt128 PoolTotalDepositCoin;

        public readonly UInt128 SwapBaseInAmount => Raw.SwapBaseInAmount;
        public readonly UInt128 SwapQuoteOutAmount => Raw.SwapQuoteOutAmount;
        public readonly ulong SwapBase2QuoteFee => Raw.SwapBase2QuoteFee;
        public readonly UInt128 SwapQuoteInAmount => Raw.SwapQuoteInAmount;
        public readonly UInt128 SwapBaseOutAmoun => Raw.SwapBaseOutAmount;
        public readonly ulong SwapQuote2BaseFee => Raw.SwapQuote2BaseFee;

        // AMM Vault
        public readonly PublicKey BaseVault => new PublicKey(Raw.BaseVault);
        public readonly PublicKey QuoteVault => new PublicKey(Raw.QuoteVault);

        // Mint
        public readonly PublicKey BaseMint => new PublicKey(Raw.BaseMint);
        public readonly PublicKey QuoteMint => new PublicKey(Raw.QuoteMint);
        public readonly PublicKey LpMint => new PublicKey(Raw.LpMint);

        // Market
        public readonly PublicKey OpenOrders => new PublicKey(Raw.OpenOrders);
        public readonly PublicKey MarketId => new PublicKey(Raw.MarketId);
        public readonly PublicKey MarketProgramId => new PublicKey(Raw.MarketProgramId);
        public readonly PublicKey TargetOrders => new PublicKey(Raw.TargetOrders);
        public readonly PublicKey WithdrawQueue => new PublicKey(Raw.WithdrawQueue);
        public readonly PublicKey LpVault => new PublicKey(Raw.LpVault);
        public readonly PublicKey Owner => new PublicKey(Raw.Owner);

        // true circulating supply without lock up
        public readonly ulong LpReserve => Raw.LpReserve;

        public readonly ulong[] Padding => Raw.Padding;
    }
}
