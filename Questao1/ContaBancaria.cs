using System;

public class ContaBancaria
{
    const double taxa = 3.5;
    public int NumeroConta { get; }
    public string TitularConta { get; set; }

    private double _saldo;
    public double Saldo => Math.Round(_saldo, 2);

    public ContaBancaria(int numeroConta, string titularConta, double depositoInicial = 0)
    {
        NumeroConta = numeroConta;
        TitularConta = titularConta;
        _saldo = depositoInicial; 
    }

    public void Depositar(double quantia)
    {
        if (quantia > 0)
        {
            _saldo += quantia;
        }
    }

    public void Sacar(double quantia)
    {
        if (quantia > 0)
        {
            _saldo -= quantia + taxa; 
        }
    }

    public string ApresentarDados()
        => $"Conta {NumeroConta}, Titular: {TitularConta}, Saldo: {Saldo.ToString("F2")}";
}
