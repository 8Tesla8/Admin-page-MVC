//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Data.SqlTypes;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;


//namespace Uspa.Domain.ModelCodeFirst
//{
//    public class Migration
//    {
//        public Migration()
//        {
//            int lang = LanguageMigration();//не ставить коментарий

//            int site = SiteMigration(); //+
//            int ban = BannerMigration(); //+

//            int usGr = UserGroupMigration();//+
//            int us = UserMigration();//+

//            int mt = MenuTypeMigration();//+
//            int menu = MenuMigration();//+ -
//            int mToM = MenuToMenuMigration();//+

//            int alb = AlbumMigration();//+
//            int img = ImageMigration();//+

//            int cat = CategoryMigration();//+
//            int cont = ContentMigration();//+
//        }

//        public int LanguageMigration() //1
//        {
            
//            List<Language> lang = new List<Language>
//            {
//                new Language { title="English" },
//                new Language { title="Russian" },
//                new Language { title="Ukrainian" }
//            };

//            int countChanges = -1;
//            using (UspaDbContextEntity newDb = new UspaDbContextEntity())
//            {
//                //newDb.Languages.AddRange(lang);
//                foreach(var item in lang)
//                {
//                    newDb.Languages.Add(item);
//                }
//                countChanges = newDb.SaveChanges();
//            }

//            return countChanges;
//        }

//        public int SiteMigration()//2 
//        {

//            int countChanges = -1;
//            using (UspaDbContextEntity newDb = new UspaDbContextEntity())
//            {
//                var langFromNewDb = newDb.Languages.ToList();

//                List<Site> sites = new List<Site>{
//                    new Site { title="Белгород-Днестровск", language = langFromNewDb[1]},   //1 risuian
//                    new Site { title="Belgorod-Dniester", language = langFromNewDb[0]},     //0 English
//                    new Site { title="Білгород-Дністровськ", language = langFromNewDb[2]},  //2 ukranian

//                    new Site { title="Бердянск", language = langFromNewDb[1]},
//                    new Site { title="Berdyansk", language = langFromNewDb[0]},
//                    new Site { title="Бердянськ", language = langFromNewDb[2]},

//                    new Site { title="Дельта-лоцман", language = langFromNewDb[1]},
//                    new Site { title="Delta-Pilot", language = langFromNewDb[0]},
//                    new Site { title="Дельта-лоцман", language = langFromNewDb[2]},

//                    new Site { title="Измаил", language = langFromNewDb[1]},
//                    new Site { title="Ishmael", language = langFromNewDb[0]},
//                    new Site { title="Ізмаїл", language = langFromNewDb[2]},

//                    new Site { title="Ильичевск", language = langFromNewDb[1]},
//                    new Site { title="Ilyichevsk", language = langFromNewDb[0]},
//                    new Site { title="Іллічівськ", language = langFromNewDb[2]},

//                    new Site { title="Мариуполь", language = langFromNewDb[1]},
//                    new Site { title="Mariupol", language = langFromNewDb[0]},
//                    new Site { title="Маріуполь", language = langFromNewDb[2]},
 
//                    new Site { title="Николаев", language = langFromNewDb[1]},
//                    new Site { title="Nikolaev", language = langFromNewDb[0]},
//                    new Site { title="Миколаїв", language = langFromNewDb[2]},

//                    new Site { title="Одесса", language = langFromNewDb[1]},
//                    new Site { title="Odessa", language = langFromNewDb[0]},
//                    new Site { title="Одеса", language = langFromNewDb[2]},

//                    new Site { title="Октябрьск", language = langFromNewDb[1]},
//                    new Site { title="Oktyabrsk", language = langFromNewDb[0]},
//                    new Site { title="Октябрьск", language = langFromNewDb[2]},

//                    new Site { title="Рени", language = langFromNewDb[1]},
//                    new Site { title="Reni", language = langFromNewDb[0]},
//                    new Site { title="Рені", language = langFromNewDb[2]},

//                    new Site { title="Скадовск", language = langFromNewDb[1]},
//                    new Site { title="Skadovsk", language = langFromNewDb[0]},
//                    new Site { title="Скадовськ", language = langFromNewDb[2]},

//                    new Site { title="Усть-Дунайск", language = langFromNewDb[1]},
//                    new Site { title="Ust-Danube", language = langFromNewDb[0]},
//                    new Site { title="Усть-Дунайськ", language = langFromNewDb[2]},

//                    new Site { title="Херсон", language = langFromNewDb[1]},
//                    new Site { title="Herson", language = langFromNewDb[0]}, 
//                    new Site { title="Херсон", language = langFromNewDb[2]},

//                    new Site { title="Южный", language = langFromNewDb[1]},
//                    new Site { title="Yuznyi", language = langFromNewDb[0]},
//                    new Site { title="Южний", language = langFromNewDb[2]},
//                };
//                //42 
//                //newDb.Sites.AddRange(sites);
//                foreach (var item in sites)
//                {
//                    newDb.Sites.Add(item);
//                }

//                countChanges = newDb.SaveChanges();
//            }
//            return countChanges;
//        }

//        //TODO site??? на какой сайт указывать
//        public int BannerMigration()//3 
//        {
//            List<ampu_banners> oldBanners = null;
//            using (uspaEntities oldDb = new uspaEntities())
//            {
//                oldBanners = oldDb.ampu_banners.ToList();
//            }

//            int countChanges = -1;
//            using (UspaDbContextEntity newDb = new UspaDbContextEntity())
//            {
//                var langFromNewDb = newDb.Languages.ToList();

//                foreach (var item in oldBanners)
//                {
//                    Banner ban = new Banner
//                    {
//                        title = item.name,
//                        url = item.clickurl,
//                        state = (item.state > 0) ? true : false,
//                    };

                   
//                    newDb.Banners.Add(ban);
//                }
                
//                countChanges = newDb.SaveChanges();
//                Console.WriteLine("Banners migration| rows=" + countChanges);
//            }
//            return countChanges;
//        }

//        public int UserGroupMigration()//4
//        {
//            List<ampu_usergroups> oldDbUserGroup = null;
//            using (uspaEntities oldDb = new uspaEntities())
//            {
//                oldDbUserGroup = oldDb.ampu_usergroups.ToList();
//            }

//            int countChanges = -1;
//            using (UspaDbContextEntity newDb = new UspaDbContextEntity())
//            {
//                foreach (var item in oldDbUserGroup)
//                {
//                    newDb.UserGroups.Add(new UserGroup { title = Encoding.UTF8.GetString(Encoding.Default.GetBytes(item.title)) });
//                }
//                countChanges = newDb.SaveChanges();
//            }
//            Console.WriteLine("UserGroup migration| rows=" + countChanges);
//            return countChanges;
//        }
  
//        public int UserMigration()//5 
//        {
//            List<UserGroup> usersGroupFromNewDb = new List<UserGroup>();
//            List<ampu_users> usersFromOldDb = new List<ampu_users>();
//            List<ampu_user_usergroup_map> oldUsersUserGroup = new List<ampu_user_usergroup_map>();

//            using (uspaEntities oldDb = new uspaEntities())
//            {
//                usersFromOldDb = oldDb.ampu_users.ToList();
//                oldUsersUserGroup = oldDb.ampu_user_usergroup_map.ToList();
//            }

//            int countChanges = -1;
//            using (UspaDbContextEntity newDb = new UspaDbContextEntity())
//            {
//                usersGroupFromNewDb = newDb.UserGroups.ToList();

//                foreach (var item in usersFromOldDb)
//                {
//                    User newUser = new User
//                    {
//                        name = Encoding.UTF8.GetString(Encoding.Default.GetBytes(item.name)),
//                        userName = Encoding.UTF8.GetString(Encoding.Default.GetBytes(item.username)),
//                        email = Encoding.UTF8.GetString(Encoding.Default.GetBytes(item.email)),
//                        //password = item.password,
//                        block = (item.block > 0) ? true : false,
//                        registerDate = (item.registerDate >= (DateTime)SqlDateTime.MinValue) ? item.registerDate : (DateTime?)null,
//                        lastVisitDate = (item.lastvisitDate >= (DateTime)SqlDateTime.MinValue) ? item.lastvisitDate : (DateTime?)null,
//                        prevId = item.id
//                    };

//                    //бери id со старой категории в которые входил пользователь
//                    var userCategory = oldUsersUserGroup.
//                        Where(ug => ug.user_id == newUser.prevId).
//                        Select(ug => new { ug.group_id }).ToList();
                    
//                    //все категории в старой базе которие больше 8 начанаются с 10 уменьшая что бы сходилось все с моими категориями
//                    List<int> listUserCategory = new List<int>();
//                    foreach(var category in userCategory)
//                    {
//                        int catId = (int)category.group_id;
//                        if (catId > 8)
//                            catId--;
//                        listUserCategory.Add(catId);
//                    }


//                    //ишю совпадения  
//                    foreach (var groupItem in usersGroupFromNewDb)
//                    {
//                        //if (userCategory.Any(x => x.group_id == groupItem.id))
//                        if (listUserCategory.Any(x => x == groupItem.id))
//                        {
//                            newUser.userGroup.Add(groupItem);
//                        }
//                    }
//                    newDb.Users.Add(newUser);
//                }
//                countChanges = newDb.SaveChanges();
//            }
//            Console.WriteLine("User migration| rows=" + countChanges);
//            return countChanges;
//        }

//        public int MenuTypeMigration()//6 
//        {
//            List<MenuType> typeMenu = new List<MenuType>
//            {
//                new MenuType { title = "Main menu" },
//                new MenuType { title = "Top menu" },
//                new MenuType { title = "Additional right menu" },
//                new MenuType { title = "Right menu" },
//                new MenuType { title = "Bottom menu" },
//                new MenuType { title = "Hidden menu" },
//                new MenuType { title = "Kunena menu" }, //чат
//            };


//            int countChanges = -1;
//            using (UspaDbContextEntity newDb = new UspaDbContextEntity())
//            {
//                //newDb.MenuType.AddRange(typeMenu);
//                foreach(var item in typeMenu)
//                {
//                    newDb.MenuType.Add(item);
//                }

//                countChanges = newDb.SaveChanges();
//            }
//            Console.WriteLine("Meny type migration| rows=" +countChanges);

//            return countChanges;
//        }

//        //TODO что делать с типом menu in ampu_menu, какую одессу брать 
//        public int MenuMigration() //7 
//        {
//            List<ampu_menu> oldMenu; 
//            using (uspaEntities oldDb = new uspaEntities())
//            {
//                oldMenu = oldDb.ampu_menu.ToList();
//                Console.WriteLine("count of old Menu: "+ oldMenu.Count);
//            }

//            int countChanges = -1;
//            using (UspaDbContextEntity newDb = new UspaDbContextEntity())
//            {
//                var newMenuType = newDb.MenuType.ToList();
//                List<string> lsitOfMenuType = new List<string>();

//                //делаю простой список меню типов меню, записываю только тип
//                foreach (var item in newMenuType)
//                {
//                    string title = item.title;

//                    var addedItem = title.Replace(" menu", "");
//                    if (addedItem.Contains(" right"))
//                    {
//                        addedItem = addedItem.Replace(" right", "");
//                    }
//                    string typeMenu = addedItem.ToLower();
//                    lsitOfMenuType.Add(typeMenu);

//                    //Console.WriteLine(typeMenu);
//                }

//                //копирую сами меню
//                foreach (var item in oldMenu)
//                {
//                    Menu newMenu = new Menu
//                    {
//                        title = Encoding.UTF8.GetString(Encoding.Default.GetBytes(item.title)),
//                        state = (item.published > 0) ? true : false,
//                        parentidPrev = (int)item.parent_id,
//                        previd = item.id,
//                    };

//                    string uncoderType = Encoding.UTF8.GetString(Encoding.Default.GetBytes(item.menutype));

//                    //Console.WriteLine(uncoderType);

//                    //для типо с название просто menu
//                    if (uncoderType.Equals("menu"))
//                    {
//                        newMenu.menuType = newMenuType[0];//main menu
//                    }
//                    //ищю нужний тип сравнивая строки
//                    else
//                    {
//                        int index = -1;
//                        int count = 0;

//                        foreach (var comparItem in lsitOfMenuType)
//                        {                    
//                            if (uncoderType.Contains(comparItem))
//                            {
//                                index = count;
//                                //Console.WriteLine("FOUND ={0} | INDEX={1}", comparItem, index);
//                                break;
//                            }
//                            count++;
//                        }

//                        if(index != -1)
//                        {
//                            newMenu.menuType = newMenuType[index];
//                        }
//                    }
            
//                    newDb.Menu.Add(newMenu);
//                }
         
//                countChanges = newDb.SaveChanges();
//            }

//            Console.WriteLine("Meny migration| rows=" + countChanges);
//            return countChanges;
//        }

//        public int MenuToMenuMigration()
//        {
//            int countChanges = 1;
//            using (UspaDbContextEntity newDb = new UspaDbContextEntity())
//            {
//                var menu = newDb.Menu.ToList();

//                for (int i = 0; i < menu.Count; i++)
//                {
//                    Menu parentMenu = null;
//                    if (menu[i].parentidPrev > 0)
//                     parentMenu = menu.FirstOrDefault(m => m.previd == menu[i].parentidPrev);
//                    //parentMenu.menu.Add(menu[i]);
//                    if(parentMenu!=null)
//                    menu[i].parentid = parentMenu.id;
//                }

//                //foreach(var item in menu)
//                //{
//                //    Menu parentMenu = menu.FirstOrDefault(m => m.previd == item.parentidPrev);
//                //    parentMenu.parent = item;
//                //}
//                countChanges = newDb.SaveChanges();
//            }
//            Console.WriteLine("MenyToMenu migration| rows=" + countChanges);
//            return countChanges;
//        }

//        //8 ampu_phocagallery_categories
//        public int AlbumMigration() 
//        {
//            List<Album> image = new List<Album>();

//            List<ampu_phocagallery_categories> oldPhotoCtg = null;
//            using (uspaEntities oldDb = new uspaEntities())
//            {
//                oldPhotoCtg = oldDb.ampu_phocagallery_categories.ToList();
//            }

//            int countChanges = -1;
//            using (UspaDbContextEntity newDb = new UspaDbContextEntity())
//            {
//                var langFromNewDb = newDb.Languages.ToList();

//                foreach (var item in oldPhotoCtg)
//                {
//                    Album album = new Album
//                    {
//                        title = Encoding.UTF8.GetString(Encoding.Default.GetBytes(item.title)),
//                        created = item.date,
//                        previd = item.id,
//                    };

//                    if (item.language.Contains("ru")) //1
//                    {
//                        album.language = langFromNewDb[1];
//                    }
//                    else if (item.language.Contains("uk")) //2
//                    {
//                        album.language = langFromNewDb[2];
//                    }
//                    else if (item.language.Contains("en")) //0
//                    {
//                        album.language = langFromNewDb[0];
//                    }
//                    else
//                    {
//                        album.language = null;
//                    }

//                    newDb.Albums.Add(album);
//                }
       
//                countChanges = newDb.SaveChanges();
//                Console.WriteLine("Album migration| rows=" + countChanges);
//            }
//            return countChanges;
//        }


//        public int ImageMigration() //9 ampu_phocagallery 
//        {
//            List<ampu_phocagallery> oldImage = null;
//            using (uspaEntities oldDb = new uspaEntities())
//            {
//                oldImage = oldDb.ampu_phocagallery.ToList();
//                Console.WriteLine("Count old images: "+ oldImage.Count);
//            }

//            int countChanges = -1;
//            using (UspaDbContextEntity newDb = new UspaDbContextEntity())
//            {
//                var album = newDb.Albums.ToList();

//                //foreach (var item in oldImage)
//                //{
//                //    Image image = new Image
//                //    {
//                //        title = Encoding.UTF8.GetString(Encoding.Default.GetBytes(item.title)),
//                //        filePath = item.filename,
//                //        created = (item.date >= (DateTime)SqlDateTime.MinValue) ? item.date : (DateTime?)null,
//                //        state = (item.published > 0) ? true : false,
//                //    };

//                //    //add in album 
//                //    //var neededAlbum = album.Where(a => a.previd == item.catid).ToList();
//                //    //image.album = neededAlbum[0];

//                //    var neededAlbum = album.FirstOrDefault(a => a.previd == item.catid);
//                //    image.album = neededAlbum;

//                //    newDb.Images.Add(image);
//                //}



//                Parallel.ForEach(oldImage, item =>
//                {
//                    Image image = new Image
//                    {
//                        title = Encoding.UTF8.GetString(Encoding.Default.GetBytes(item.title)),
//                        filePath = item.filename,
//                        created = (item.date >= (DateTime)SqlDateTime.MinValue) ? item.date : (DateTime?)null,
//                        state = (item.published > 0) ? true : false,
//                    };


//                    var neededAlbum = album.FirstOrDefault(a => a.previd == item.catid);
//                    image.album = neededAlbum;

//                    //In the function
//                    lock (lockMe)
//                    {
//                        newDb.Images.Add(image);
//                    }
//                }
//                );


//                countChanges = newDb.SaveChanges();
//                Console.WriteLine("Image migration| rows=" + countChanges);
//            }
//            return countChanges;
//        }
//        Object lockMe = new Object();

//        //TODO связь категорий и allowedGroup        
//        public int CategoryMigration() 
//        {
//            List<ampu_categories> categoryFromOldDb = new List<ampu_categories>();
//            using (uspaEntities oldDb = new uspaEntities())
//            {
//                categoryFromOldDb = oldDb.ampu_categories.ToList();
//            }

//            int countChanges = -1;
//            using (UspaDbContextEntity newDb = new UspaDbContextEntity())
//            {
//                var langFromNewDb = newDb.Languages.ToList();
//                var usersFromNewDb = newDb.Users.ToList();

//                foreach (var item in categoryFromOldDb)
//                {
//                    Category category = new Category
//                    {
//                        title = Encoding.UTF8.GetString(Encoding.Default.GetBytes(item.title)),
//                        createdTime = item.created_time,
//                        published = (item.published > 0) ? true : false,
//                        previd = item.id
//                    };

//                    if (item.language.Contains("ru")) //1
//                    {
//                        category.language = langFromNewDb[1];
//                    }
//                    else if (item.language.Contains("uk")) //2
//                    {
//                        category.language = langFromNewDb[2];
//                    }
//                    else if (item.language.Contains("en")) //0
//                    {
//                        category.language = langFromNewDb[0];
//                    }
//                    else
//                    {
//                        category.language = null;
//                    }

//                    category.createdByUser = usersFromNewDb.
//                        FirstOrDefault(u => item.created_user_id == u.prevId);

//                    newDb.Categories.Add(category);
//                }
              
//                countChanges = newDb.SaveChanges();
//                Console.WriteLine("Category migration| rows=" + countChanges);
//            }

//            return countChanges;
//        }

//        //TODO нет ссылки на сайт
//        public int ContentMigration()
//        {
//            List<ampu_content> contentFromOldDb = new List<ampu_content>();
//            using (uspaEntities oldDb = new uspaEntities())
//            {
//                contentFromOldDb = oldDb.ampu_content.AsParallel().ToList();
//                Console.WriteLine("Count old content = " + contentFromOldDb.Count);
//            }

//            int countChanges = -1;
//            using (UspaDbContextEntity newDb = new UspaDbContextEntity())
//            {
//                var category = newDb.Categories.ToList();
//                var users = newDb.Users.ToList();
               
//                //foreach(var item in contentFromOldDb)
//                Parallel.ForEach(contentFromOldDb, item =>
//                {
//                    Content content = new Content
//                    {
//                        title = Encoding.UTF8.GetString(Encoding.Default.GetBytes(item.title)),
//                        introtext = Encoding.UTF8.GetString(Encoding.Default.GetBytes(item.introtext)),
//                        fulltext = Encoding.UTF8.GetString(Encoding.Default.GetBytes(item.fulltext)),
//                        state = (item.state > 0) ? true : false,
//                        created = (item.created >= (DateTime)SqlDateTime.MinValue) ? item.created : (DateTime?)null,
//                        modified = (item.modified >= (DateTime)SqlDateTime.MinValue) ? item.modified : (DateTime?)null,
//                        checkIn = (item.publish_up >= (DateTime)SqlDateTime.MinValue) ? item.publish_up : (DateTime?)null,
//                        checkOut = (item.publish_down >= (DateTime)SqlDateTime.MinValue) ? item.publish_down : (DateTime?)null,

//                        category = category.FirstOrDefault(c => c.previd == item.catid),
//                    };

//                    if (item.created_by != 0)
//                        content.createdByUser = users.FirstOrDefault(u => u.prevId == item.created_by);

//                    if (item.modified_by != 0)
//                        content.modifiedByUser = users.FirstOrDefault(u => u.prevId == item.modified_by);

//                    //In the function
//                    lock (lockMe)
//                    {
//                        newDb.Contents.Add(content);
//                        //newDb.SaveChanges();
//                    }
//                }
//                );//parallel end

//                countChanges = newDb.SaveChanges();
//                Console.WriteLine("Content migration| rows=" + countChanges);
//            }

//            return countChanges;
//        }


//    }
//}
