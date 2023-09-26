public class Account {
    private String custID, custName, custAddress, custPhone;
    protected double balance;

    public Account(String custID, String custName, String custAddress, String custPhone) {
        this.custID = custID;
        this.custName = custName;
        this.custAddress = custAddress;
        this.custPhone = custPhone;
    }

    public Account(){

    }

    public String getCustID() {
        return custID;
    }

    public void setCustID(String custID) {
        this.custID = custID;
    }

    public String getCustName() {
        return custName;
    }

    public void setCustName(String custName) {
        this.custName = custName;
    }

    public String getCustAddress() {
        return custAddress;
    }

    public void setCustAddress(String custAddress) {
        this.custAddress = custAddress;
    }

    public String getCustPhone() {
        return custPhone;
    }

    public void setCustPhone(String custPhone) {
        this.custPhone = custPhone;
    }

    public double getBalance() {
        return balance;
    }

    public void setBalance(double balance) {
        this.balance = balance;
    }

    public void add(double number){
        setBalance(this.balance + number);
    }
    
    public void subtract(double number){
        setBalance(balance - number);
    }

    public void yearlyInterest(){
        setBalance(1.03 * this.balance);
    }

    @Override
    public String toString() {
        String accountDetailsFormat = "\nCustomer ID: %s\nCustomer Name: %s\nCustomer Address: %s\nCustomer Phone: %s\nCustomer Balance: %s";
        return String.format(accountDetailsFormat, this.custID, this.custName, this.custAddress, this.custPhone, this.balance);
    }
}
