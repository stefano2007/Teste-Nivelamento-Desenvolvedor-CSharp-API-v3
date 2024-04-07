using System;
using System.Collections.Generic;
using System.Linq;

namespace Questao1
{
    public class ContaBancaria {
        public ContaBancaria(int numero, string titular, double depositoInicial)
        {
            Numero = numero;
            Titular = titular;
            Deposito(depositoInicial);
        }

        public ContaBancaria(int numero, string titular)
        {
            Numero = numero;
            Titular = titular;
        }

        public int Numero { get; private set; }
        public string Titular { get; private set; }
        private List<TransacaoBancaria> _transacoes { get; set; }
        private void AddTransacao(TransacaoBancaria transacaoBancaria)
        {
            _transacoes ??= new List<TransacaoBancaria>();
            _transacoes.Add(transacaoBancaria);
        }

        public void Deposito(double quantia)
        {
            AddTransacao(TransacaoBancaria.Deposito(quantia));
        }

        public void Saque(double quantia)
        {
            AddTransacao(TransacaoBancaria.Saque(quantia));
        }

        public void SetTitula(string titular)
        {
            Titular = titular;
        }

        public double SaldoAtual()
        {
            _transacoes ??= new List<TransacaoBancaria>();
            return _transacoes.Sum(x => ValorTransacao(x));
        }

        private double ValorTransacao(TransacaoBancaria transacaoBancaria)
        {
            double valor = transacaoBancaria.Valor + transacaoBancaria.ValorTaxa;
            return transacaoBancaria.TipoTransacaoBancaria.Equals(TipoTransacaoBancaria.Deposito)
                 ? valor
                 : valor * -1;
        }

        public override string ToString()
        {
            //TODO Resolver , por ponto casa decimais
            return $"Conta {Numero}, Titular: {Titular}, Saldo: $ {SaldoAtual():N2}";
        }
    }

    public class TransacaoBancaria
    {
        private const double VALOR_TRANSACAO_SAQUE = 3.5;
        public TransacaoBancaria(double valor, TipoTransacaoBancaria tipoTransacaoBancaria)
        {
            Valor = valor;
            TipoTransacaoBancaria = tipoTransacaoBancaria;
            ValorTaxa = tipoTransacaoBancaria.Equals(TipoTransacaoBancaria.Saque)
                        ? VALOR_TRANSACAO_SAQUE
                        : 0;
            DataTransacao = DateTime.UtcNow;
        }
        public double Valor { get; private set; }
        public double ValorTaxa { get; private set; }
        public TipoTransacaoBancaria TipoTransacaoBancaria { get; private set; }
        public DateTime DataTransacao { get; set; }
        public static TransacaoBancaria Deposito(double valor)
        {
            return new(valor: valor, tipoTransacaoBancaria: TipoTransacaoBancaria.Deposito);
        }
        public static TransacaoBancaria Saque(double valor)
        {
            return new(valor: valor, tipoTransacaoBancaria: TipoTransacaoBancaria.Saque);
        }
    }
    public enum TipoTransacaoBancaria
    {
        Deposito = 0,
        Saque = 1
    }
}
