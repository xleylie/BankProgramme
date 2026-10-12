using BankProgramme;

//Part 1
BankAccount account = new BankAccount();

Thread[] threads = new Thread[10];
for (int i = 0; i < 10; i++)
{
    threads[i] = new Thread(() =>
    {
        for (int j = 0; j < 10000; j++)
        {
            account.Deposit(1);
        }
    });
    threads[i].Start();
}

foreach (Thread thread in threads)
{
    thread.Join();
}

Console.WriteLine(account.GetBalance());

//Баланс должен получится 101_000

/*Потому что несколько потоков одновременно изменяют баланс, 
и некоторые пополнения перезаписывают друг друга. 
Это называется Race Condition
*/

/*Потому что Balance += amount не выполняется как одно неделимое действие. 
Если несколько потоков изменяют баланс одновременно, они могут перезаписать результаты друг друга, 
из-за чего часть пополнений потеряется
*/

//Part 2

BankAccount account1 = new BankAccount();
object _lock = new object();
Thread[] threads1 = new Thread[10];
for (int i = 0; i < 10; i++)
{
    threads1[i] = new Thread(() =>
    {
        for (int j = 0; j < 10000; j++) { lock (_lock) { account1.Deposit(1); } }
    });
    threads1[i].Start();
}

foreach (Thread thread in threads1)
{
    thread.Join();
}

Console.WriteLine(account1.GetBalance());

//Part 3

BankAccount account2 = new BankAccount();

Thread[] threads2 = new Thread[10];
for (int i = 0; i < 10; i++)
{
    threads2[i] = new Thread(() =>
    {
        for (int j = 0; j < 10000; j++) { Interlocked.Increment(ref account2.Balance); } 
    });
    threads2[i].Start();
}

foreach (Thread thread in threads2)
{
    thread.Join();
}

Console.WriteLine(account2.GetBalance());