using System;
using System.Linq;
using System.Collections.Generic;

namespace ByteBank
{
    public class Program
    {

        static void ShowMenu()
        {
            Console.WriteLine("\n===========================================\n");
            Console.WriteLine("1 - Inserir novo usuário");
            Console.WriteLine("2 - Deletar um usuário");
            Console.WriteLine("3 - Listar todas as contas registradas");
            Console.WriteLine("4 - Detalhes de um usuário");
            Console.WriteLine("5 - Quantia armazenada no banco");
            Console.WriteLine("6 - Manipular a conta");
            Console.WriteLine("0 - Para sair do programa");
            Console.WriteLine("\n===========================================\n");
            Console.Write("Digite a opção desejada: ");
        }

        static void InserirUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {
            Console.Write("Digite o cpf: ");
            cpfs.Add(Console.ReadLine());
            Console.Write("Digite o nome: ");
            titulares.Add(Console.ReadLine());
            Console.Write("Digite a senha: ");
            senhas.Add(Console.ReadLine());
            saldos.Add(0);
            Console.WriteLine("Usuário inserido com sucesso!");
        }

        static void DeletarUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {
            Console.Write("Digite o cpf: ");
            string cpfParaDeletar = Console.ReadLine();
            int indexParaDeletar = cpfs.FindIndex(cpf => cpf == cpfParaDeletar);

            if (indexParaDeletar == -1)
            {
                Console.WriteLine("Não foi possível deletar esta Conta");
                Console.WriteLine("MOTIVO: Conta não encontrada.");
            }

            cpfs.Remove(cpfParaDeletar);
            titulares.RemoveAt(indexParaDeletar);
            senhas.RemoveAt(indexParaDeletar);
            saldos.RemoveAt(indexParaDeletar);

            Console.WriteLine("Conta deletada com sucesso");
        }

        static void ListarContas(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            for (int i = 0; i < cpfs.Count; i++)
            {
                MostrarConta(i, cpfs, titulares, saldos);
            }
        }

        static void MostrarUsuario(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.Write("Digite o cpf: ");
            string cpfParaApresentar = Console.ReadLine();
            int indexParaApresentar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

            if (indexParaApresentar == -1)
            {
                Console.WriteLine("Não foi possível apresentar esta Conta");
                Console.WriteLine("MOTIVO: Conta não encontrada.");
            }

            MostrarConta(indexParaApresentar, cpfs, titulares, saldos);
        }

        static void MostrarTotalBanco(List<double> saldos)
        {
            Console.WriteLine($"Total acumulado no banco: {saldos.Sum()}");
        }

        static void ManipularConta(List<string> cpfs, List<double> saldos, List<string> senhas)
        {

            Console.Write("Digite o cpf: ");
            int menu;
            string contaParaApresentar = Console.ReadLine();
            int indexContaApresentar = cpfs.FindIndex(cpf => cpf == contaParaApresentar);

            if (indexContaApresentar == -1)
            {
                Console.WriteLine("Não foi possível apresentar esta Conta");
                Console.WriteLine("MOTIVO: Conta não encontrada.");
            }
            else {
                Console.Write("Digite sua senha de acesso: ");
                string senhaApresentar = Console.ReadLine();
                if (senhaApresentar.Equals(senhas[indexContaApresentar])) {
                    do
                    {
                        Console.WriteLine("\n===========================================\n");
                        Console.WriteLine("0 - Menu principal");
                        Console.WriteLine("1 - Saque");
                        Console.WriteLine("2 - Depósito");
                        Console.WriteLine("3 - Transferência");
                        Console.WriteLine("\n===========================================\n");
                        Console.Write("Digite a opção desejada: ");
                        menu = int.Parse(Console.ReadLine());
                        switch (menu)
                        {
                            case 0:
                                return;
                                //break;
                            case 1:
                                Console.Write("Digite o valor a ser sacado: ");
                                double valorSacar = double.Parse(Console.ReadLine());
                                double auxSacar = saldos[indexContaApresentar];
                                if (valorSacar < auxSacar)
                                {
                                    saldos[indexContaApresentar] = auxSacar - valorSacar;
                                }
                                else
                                {
                                    Console.WriteLine("Saldo insuficiente!");
                                }
                                break;
                            case 2:
                                Console.Write("Digite o valor a ser depositado: ");
                                double valorDepositar = double.Parse(Console.ReadLine());
                                double auxDepositar = saldos[indexContaApresentar];
                                if (valorDepositar > 0)
                                {
                                    saldos[indexContaApresentar] = auxDepositar + valorDepositar;
                                }
                                else
                                {
                                    Console.WriteLine("Valor inválido!");
                                }
                                break;
                            case 3:
                                Console.WriteLine("Digite o valor a ser transferido: ");
                                double valorTransferir = double.Parse(Console.ReadLine());
                                double auxTransferir = saldos[indexContaApresentar];
                                if (valorTransferir < auxTransferir)
                                {
                                    saldos[indexContaApresentar] = auxTransferir - valorTransferir;
                                }
                                else
                                {
                                    Console.WriteLine("Saldo insuficiente!");
                                }
                                Console.WriteLine("Digite o CPF para conta de transferência: ");
                                string contaTransferir = Console.ReadLine();
                                int indexContaTransferir = cpfs.FindIndex(cpf => cpf == contaTransferir);

                                if (indexContaTransferir == -1)
                                {
                                    Console.WriteLine("Não foi possível apresentar esta Conta");
                                    Console.WriteLine("MOTIVO: Conta não encontrada.");
                                    saldos[indexContaApresentar] += valorTransferir;
                                }
                                else
                                {
                                    saldos[indexContaTransferir] += valorTransferir;
                                }

                                break;
                            default:
                                Console.WriteLine("\nOpção inválida! Tente mais uma vez...\n");
                                break;
                        }
                    } while (menu != 0);
                } else {
                    Console.WriteLine("Senha incorreta!");
                }
            }
        }

        static void MostrarConta(int index, List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.WriteLine($"CPF = {cpfs[index]} | Titular = {titulares[index]} | Saldo = R$ {saldos[index]:F2}");
        }

        public static void Main(string[] args)
        {

            List<string> cpfs = new List<string>();
            List<string> titulares = new List<string>();
            List<string> senhas = new List<string>();
            List<double> saldos = new List<double>();

            Console.WriteLine("\nSeja bem vindo(a)!");
            ShowMenu();
            int opcao = int.Parse(Console.ReadLine());
            //Console.Clear();
            do
            {
                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("\nFechando o programa...\n");
                        return;
                        //break;
                    case 1:
                        //Console.WriteLine("\nInserir novo usuário\n");
                        InserirUsuario(cpfs, titulares, senhas, saldos);
                        break;
                    case 2:
                        //Console.WriteLine("\nDeletar usuário\n");
                        DeletarUsuario(cpfs, titulares, senhas, saldos);
                        break;
                    case 3:
                        //Console.WriteLine("\nTodas as contas\n");
                        ListarContas(cpfs, titulares, saldos);
                        break;
                    case 4:
                        //Console.WriteLine("\nDetalhes da conta\n");
                        MostrarUsuario(cpfs, titulares, saldos);
                        break;
                    case 5:
                        //Console.WriteLine("\nTotal armazenado no banco\n");
                        MostrarTotalBanco(saldos);
                        break;
                    case 6:
                        //implementar
                        Console.WriteLine("\nManipular conta\n");
                        ManipularConta(cpfs, saldos, senhas);
                        break;
                    default:
                        Console.WriteLine("\nOpção inválida! Tente mais uma vez...\n");
                        break;
                }
                ShowMenu();
                opcao = int.Parse(Console.ReadLine());
                //Console.Clear();
            } while (opcao != 0);
        }
    }
}