using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using POS.DataAccess.Repository.IRepository;
using POS.Models.Models;

namespace POS.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

         public ProductController (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        [Route("~/Units/")]
        public JsonResult getAllUnit()
        {
            IEnumerable<Unit> list = _unitOfWork.Unit.GetAll();
            return Json(new { success = true, message = list });
        }
      
        public class MySelectListItem
        {
            public string Name { get; set; }
            public string Code { get; set; }
        }

        public class CategoryModel
        {
            public MySelectListItem category { get; set; }
            public List<MySelectListItem> subcategories { get; set; }

        }

        public class ProductVM
        {
            
            public List<CategoryModel> categories { get; set; }
            public IEnumerable<MySelectListItem> manufacturers { get; set; }
            public IEnumerable<MySelectListItem> units { get; set; }

        }


        [Route("~/Product/")]
        [Route("~/Product/index")]
        public  IActionResult  product_Index()
        {
         
            string trade_code = getTrade();
            string client_code = getClient();
            IEnumerable<Product> list = _unitOfWork.Product.GetAll(u=>u.trade_code ==trade_code && u.client_code == client_code);
            return Json(new { success = true, message = list });
        }

    





        [Route("~/Product/DropDown")]
        public async Task<IActionResult> product_dropdown()
        {
            
            try

            {
                string trade_code = getTrade();

                ProductVM productVM = new ProductVM();
                IEnumerable<Category> CatList = await _unitOfWork.Category.GetAllAsync(u=>u.trade_code == trade_code);
                IEnumerable<Unit> UnitList = _unitOfWork.Unit.GetAll();
                IEnumerable<Manufacturer> ManList = _unitOfWork.Manufacturer.GetAll(u => u.trade_code == trade_code);

                CatList = CatList.OrderBy(u => u.name);
                productVM.categories = new List<CategoryModel>();
                foreach(Category cat in CatList)
                {
                    CategoryModel cm = new CategoryModel();
                    cm.category = new MySelectListItem();
                    cm.category.Name = cat.name;
                    cm.category.Code = cat.code;
                    var SubCategories = _unitOfWork.SubCategory.GetAll(u => u.trade_code == trade_code && u.category_code == cat.code);
                    cm.subcategories = (from c in SubCategories
                                       select (new MySelectListItem {
                                           Name = c.name,
                                           Code =c.code
                                       })).OrderBy(u=>u.Name).ToList();
                    productVM.categories.Add(cm);
                    
                }
                
                productVM.manufacturers = ManList.Select(i => new MySelectListItem
                {
                    Name = i.name,
                    Code = i.code

                });
                productVM.units = UnitList.Select(i => new MySelectListItem
                {
                    Name = i.unit,
                    Code = i.id.ToString()


                });

                return Json(new { success = true, message = productVM });
            }
            catch(Exception e)
            {
                return Json(new { success = false, message = e.Message });

            }
        }

        [HttpPost]
        [Route("~/Product/add")]
        public async Task<IActionResult> Upsert([FromBody] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string trade_code = getTrade();
                    string client_code = getClient();
                    if (product.id == 0)
                    {
                       
                        string p_code = _unitOfWork.Product.getProductCode(product.category_code,client_code,trade_code);
                        Category cat = await  _unitOfWork.Category.GetFirstOrDefaultAsync(u => u.code == product.category_code && u.trade_code == trade_code );
                        Manufacturer man = _unitOfWork.Manufacturer.GetFirstOrDefault(u => u.code== product.manufacturer_code && u.trade_code == trade_code);
                        product.manufacturer = man.name;
                        product.category = cat.name;
                        if(product.subcategory_code!= null)
                        {
                            product.subcategory = _unitOfWork.SubCategory.GetFirstOrDefault(u => u.code == product.subcategory_code && u.trade_code == trade_code).name;
                    
                        }
                        product.product_code = p_code;
                        if (product.barcode == null)
                        {
                            product.barcode = p_code;
                        }
                        product.product_name = product.product_name.ToUpper();
                        product.client_code = client_code;
                        product.trade_code = trade_code;

                      _unitOfWork.Product.Add(product);
                        POSLog pOSLog = _unitOfWork.POSLog.GetFirstOrDefault(u =>  u.client_code == client_code && u.trade_code == trade_code);
                        pOSLog.product_code = p_code;
                        _unitOfWork.POSLog.Update(pOSLog);
                    }
                    else
                    {

                        product.product_name = product.product_name.ToUpper();
                        Category cat = await _unitOfWork.Category.GetFirstOrDefaultAsync(u => u.code == product.category_code && u.trade_code == trade_code);
                        Manufacturer man = _unitOfWork.Manufacturer.GetFirstOrDefault(u => u.code == product.manufacturer_code && u.trade_code == trade_code);
                        product.manufacturer = man.name;
                        product.category = cat.name;
                        product.product_name = product.product_name.ToUpper();
                        if (product.subcategory_code != null)
                        {
                            product.subcategory = _unitOfWork.SubCategory.GetFirstOrDefault(u => u.code == product.subcategory_code && u.trade_code == trade_code).name;

                        }
                        _unitOfWork.Product.Update(product);
                    }


                    _unitOfWork.Save();
                    Product product1 = _unitOfWork.Product.GetFirstOrDefault(u => u.product_code == product.product_code && u.trade_code == trade_code);
                    return Json(new { success = true, message = product });

                }
                else
                {
                    return Json(new { success = false, message = "Add failed!!" });
                }
            }
            catch(Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
          

        }


        //[HttpDelete]
        //[Route("product/delete/{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var objFromDb = _unitOfWork.Product.Get(id);
        //    if (objFromDb == null)
        //    {
        //        return Json(new { success = false, message = "Error while deleting" });
        //    }
        //    _unitOfWork.Product.Remove(objFromDb);
        //    _unitOfWork.Save();
        //    return Json(new { success = true, message = "Delete Successful" });


        //}    
    }
}
