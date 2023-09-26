import java.util.ArrayList;
import java.util.Scanner;

public class AccountDriver {
    public static void main(String args[]) {

        Scanner in = new Scanner(System.in);
        AccountDriver ad = new AccountDriver();
        ArrayList<Account> accounts = new ArrayList<Account>();
        int count = 0;
        System.out.println("\nEnter user account details:");
        while (count < 10) {
            accounts = ad.addAccount(accounts, in);
            count++;
        }

        boolean isValid = true;
        while (isValid) {
            System.out.println(
                    "\nAccount Management Menu:\n1. Add account\n2. Display individual account\n3. Display all accounts\n4. Deposit to individual account\n5. Withdraw from individual account\n6. Financial year ending\n7. Exit\n");
            int ch = in.nextInt();

            switch (ch) {
                case 1:
                    accounts = ad.addAccount(accounts, in);
                    break;
                case 2:
                    System.out.print("Enter Customer ID: ");
                    String customerID = in.next();
                    Account acc = ad.getAccount(customerID, accounts);
                    System.out.println(acc.toString());
                    break;
                case 3:
                    ad.displayAllAccounts(accounts);
                    break;
                case 4:
                    System.out.print("Enter Customer ID: ");
                    String custId = in.next();
                    System.out.print("Enter Deposit Amount: ");
                    double amt = in.nextDouble();
                    accounts = ad.deposit(accounts, custId, amt);
                    System.out.println("Deposit successful");
                    break;
                case 5:
                    System.out.print("Enter Customer ID: ");
                    String custId_ = in.next();
                    System.out.print("Enter Withdrawal Amount: ");
                    double amt_ = in.nextDouble();
                    accounts = ad.withdraw(accounts, custId_, amt_);
                    System.out.println("Withdrawal successful");
                    break;
                case 6:
                    accounts = ad.financialYearEnding(accounts);
                    System.out.println("Interest added");
                    break;
                case 7:
                    System.out.println("Closing");
                    isValid = false;
                    break;
                default:
                    System.out.println("Invalid Menu Option!");
            }
        }
    }

    public ArrayList<Account> addAccount(ArrayList<Account> accounts, Scanner in) {
        System.out.print("\nCustomer ID: ");
        String custID = in.nextLine();
        System.out.print("Customer Name: ");
        String custName = in.nextLine();
        System.out.print("Customer Address: ");
        String custAddress = in.nextLine();
        System.out.print("Customer Phone: ");
        String custPhone = in.nextLine();

        Account account = new Account(custID, custName, custAddress, custPhone);
        accounts.add(account);

        return accounts;
    }

    public Account getAccount(String custId, ArrayList<Account> accounts) {
        for (Account acc : accounts) {
            if (acc.getCustID().equals(custId))
                return acc;
        }
        return null;
    }

    public void displayAllAccounts(ArrayList<Account> accounts) {
        System.out.println();
        String format = "%-20s%-20s%-20s%-20s%-20s";
        System.out.printf(format, "Customer ID", "Customer Name", "Customer Address", "Customer Phone",
                "Customer Balance");
        System.out.println();
        for (Account acc : accounts) {
            System.out.printf(format, acc.getCustID(), acc.getCustName(), acc.getCustAddress(), acc.getCustPhone(),
                    acc.getBalance());
            System.out.println();
        }
    }

    public ArrayList<Account> withdraw(ArrayList<Account> accounts, String custId, double amount) {
        for (Account acc : accounts) {
            if (acc.getCustID().equals(custId)) {
                acc.subtract(amount);
                return accounts;
            }
        }
        System.out.println("Customer ID not found.");
        return accounts;
    }

    public ArrayList<Account> deposit(ArrayList<Account> accounts, String custId, double amount) {
        for (Account acc : accounts) {
            if (acc.getCustID().equals(custId)) {
                acc.add(amount);
                return accounts;
            }
        }
        System.out.println("Customer ID not found.");
        return accounts;
    }

    public ArrayList<Account> financialYearEnding(ArrayList<Account> accounts) {
        for (Account acc : accounts) {
            acc.yearlyInterest();
        }
        return accounts;
    }
}
