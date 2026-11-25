using InventoryManagementModel. Models; 
using System. Data. SqlClient; 
using System; 
using System. Collections. Generic; 
using System. Linq; 
using System. Web; 
using System. Web. Mvc; 
using System. Security. Cryptography. X509Certificates; 
namespace InventoryManagementModel. Controllers 
{ 
public class ProductController : Controller 
{ 
string cs =@"Data Source=(localdb)\MSSQLLocalDB; Initial 
Catalog=InventoryDB; Integrated Security=True"; 
{ 
public ActionResult Index() 
List<Product> products = new List<Product>(); 
using (SqlConnection conn = new SqlConnection(cs)) 
{ 
string query = "SELECT * FROM Products"; 
SqlCommand cmd = new SqlCommand(query, conn); 
conn. Open(); 
SqlDataReader dr = cmd. ExecuteReader(); 
while (dr. Read()) 
{ 
products. Add(new Product 
{ 
ProductId = Convert. ToInt32(dr["ProductId"] ), 
ProductName = dr["ProductName"] . ToString(), 
Quantity = Convert. ToInt32(dr["Quantity"] ), 
Price = Convert. ToDecimal(dr["Price"] ) 
}); 
} 
} 
return View(products); 
} 
public ActionResult Create() 
{ 
return View(); 
} 
//Crete : save product 
[HttpPost] 
public ActionResult Create(Product p) 
{ 
using (SqlConnection conn = new SqlConnection(cs)) 
{ 
string query = @"INSERT INTO Products 
(ProductId, ProductName, Quantity, Price) 
VALUES (@ProductId, @ProductName, @Quantity, @Price)"; 
SqlCommand cmd = new SqlCommand(query, conn); 
// FIX THE TYPOS HERE (Change @ProuctId to @ProductId, etc. ): 
cmd. Parameters. AddWithValue("@ProductId", p. ProductId); 
cmd. Parameters. AddWithValue("@ProductName", p. ProductName); 
cmd. Parameters. AddWithValue("@Quantity", p. Quantity); 
cmd. Parameters. AddWithValue("@Price", p. Price); 
conn. Open(); 
cmd. ExecuteNonQuery(); 
} 
return RedirectToAction("Index"); 
} 
// 1. GET: Product/Edit/5 
[HttpGet] 
public ActionResult Edit(int id) 
{ 
Product p = new Product(); 
using (SqlConnection conn = new SqlConnection(cs)) 
{ 
string query = "SELECT * FROM Products WHERE ProductId=@ProductId"; 
SqlCommand cmd = new SqlCommand(query, conn); 
cmd. Parameters. AddWithValue("@ProductId", id); 
conn. Open(); 
SqlDataReader dr = cmd. ExecuteReader(); 
if (dr. Read()) 
{ 
p. ProductId = Convert. ToInt32(dr["ProductId"] ); 
p. ProductName = dr["ProductName"] . ToString(); 
p. Quantity = Convert. ToInt32(dr["Quantity"] ); 
p. Price = Convert. ToDecimal(dr["Price"] ); 
} 
else { return HttpNotFound(); } 
} 
return View(p); 
} 
// 2. POST: Product/Edit 
[HttpPost] 
public ActionResult Edit(Product p) 
{
    using (SqlConnection conn = new SqlConnection(cs)) 
{ 
string query = @"UPDATE Products SET 
ProductName=@ProductName, 
Quantity=@Quantity, 
Price=@Price 
WHERE ProductId=@ProductId"; 
SqlCommand cmd = new SqlCommand(query, conn); 
cmd. Parameters. AddWithValue("@ProductId", p. ProductId); 
cmd. Parameters. AddWithValue("@ProductName", p. ProductName); 
cmd. Parameters. AddWithValue("@Quantity", p. Quantity); 
cmd. Parameters. AddWithValue("@Price", p. Price); 
conn. Open(); 
cmd. ExecuteNonQuery(); 
} 
return RedirectToAction("Index"); 
} 
[HttpPost] 
public ActionResult Delete(int id) 
{ 
using (SqlConnection conn = new SqlConnection(cs)) 
{ 
// FIX TYPO: Change @ProuductId to @ProductId 
string query = "DELETE FROM Products WHERE ProductId=@ProductId"; 
SqlCommand cmd = new SqlCommand(query, conn); 
// FIX TYPO: Change @ProuctId to @ProductId 
cmd. Parameters. AddWithValue("@ProductId", id); 
conn. Open(); 
cmd. ExecuteNonQuery(); 
} 
return RedirectToAction("Index"); 
} 
} 
}