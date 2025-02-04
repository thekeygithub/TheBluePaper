﻿using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using System;
using System.Numerics;

namespace TransferContract
{
    public class TransferContract : SmartContract
    {
        //合约地址
        public static readonly byte[] Owner = "AcesbhiJs1orCB4ty5c4Pi2SBRiMUspvhL".ToScriptHash();
        //易保
        public static readonly byte[] yibao = "AbyKBcoJPdW46DLGUUHJJ8vtVntrF6UirT".ToScriptHash();
        //运营商
        public static readonly byte[] yys = "AQtAQPyzCRLTaZhsthdZgTBfq4ytr4JEf9".ToScriptHash();
        //医院
        public static readonly byte[] yiyuan = "AZspVZzkmB6GsDKnsbB7fABacUq4SuMwoc".ToScriptHash();
        [Appcall("132947096727c84c7f9e076c90f08fec3bc17f18")]//ScriptHash
        public static extern bool THEKEYCall(string operation, object[] args);
        public static object Main(string operation, object[] args)
        {
            if (operation == "allotTKY") return allotTKY(args);
            
            return "fault";
        }
        public static bool allotTKY(object[] args)
        {
            bool isSucc = true;
            //构造参数
            object[] args2 = new object[3];
            string transferInfo = "";
            args2[0] = Owner;
            if ((BigInteger)args[1] > 0)//判断是否需要给易保转
            {
                args2[1] = yibao;
                args2[2] = 2 * 100000000;
                isSucc = THEKEYCall("transfer", args2);//给易保转2个
                args2[1] = yys;
                args2[2] = 1 * 100000000;
                isSucc = THEKEYCall("transfer", args2);
                transferInfo = "[{\"type\": \"yb\",\"Fee\": \"2 \",\"from\":\"AcesbhiJs1orCB4ty5c4Pi2SBRiMUspvhL\",\"to\":\"AbyKBcoJPdW46DLGUUHJJ8vtVntrF6UirT\"},{\"type\": \"yys\",\"Fee\":\"1\",\"from\":\"AcesbhiJs1orCB4ty5c4Pi2SBRiMUspvhL\",\"to\":\"AQtAQPyzCRLTaZhsthdZgTBfq4ytr4JEf9\"}]";
            }
            else
            {
                args2[1] = yiyuan;
                args2[2] = 3 * 100000000;
                isSucc = THEKEYCall("transfer", args2);
                transferInfo = "[{\"type\": \"yy\",\"Fee\":\"3\",\"from\":\"AcesbhiJs1orCB4ty5c4Pi2SBRiMUspvhL\",\"to\":\"AZspVZzkmB6GsDKnsbB7fABacUq4SuMwoc\"}]";
            }
            Storage.Put(Storage.CurrentContext, (string)args[0], transferInfo);
            return isSucc;
        }

        public static byte[] getInfo(string key)
        {
            return Storage.Get(Storage.CurrentContext, key);
        }
    }
}
