using GameWeb.Models;
using GameWeb.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameWeb.Data
{
    public static class SeedData
    {
        public static async Task SeedDb(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            await SeedRoles(serviceProvider);
            await SeedUsers(serviceProvider);
            await SeedGames(serviceProvider);
            await SeedNews(serviceProvider);

            await context.SaveChangesAsync();
        }

        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            var roleNames = typeof(RoleNames).GetFields();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            foreach (var r in roleNames)
            {
                var role = r.GetValue(null).ToString();

                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task SeedUsers(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            if (!userManager.Users.Any())
            {
                var users = new List<ApplicationUser>
                {
                    new ApplicationUser
                    {
                        UserName = "Admin",
                        Email = "admin@gameweb.com",
                    },
                    new ApplicationUser
                    {
                        UserName = "User",
                        Email = "user@gameweb.com",
                    },
                    new ApplicationUser
                    {
                        UserName = "Editor",
                        Email = "editor@gameweb.com",
                    },
                    new ApplicationUser
                    {
                        UserName = "Publisher",
                        Email = "publisher@gameweb.com",
                    },
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");

                    string role = null;

                    switch (user.UserName)
                    {
                        case "Admin":
                            role = RoleNames.AdminRole;
                            break;
                        case "Editor":
                            role = RoleNames.NewsCreatorRole;
                            break;
                        case "Publisher":
                            role = RoleNames.GamePublisherRole;
                            break;
                    }

                    if (role != null) await userManager.AddToRoleAsync(user, role);
                }
            }
        }

        public static async Task SeedGames(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            if (context.Game.Any())
            {
                return;
            }

            var users = userManager.Users.ToList();

            // thank you ChatGPT for those forum posts ;) 
            var games = new List<Game>
                {
                    new Game
                    {
                        Name = "Minecraft",
                        ReleaseDate = new DateTime(2011, 11, 18),
                        Platform = "PC, Xbox, PlayStation, Android",
                        Publisher = "Mojang",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer ut mattis dolor. " +
                        "Curabitur accumsan sapien quam, vel volutpat lacus euismod eget. Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Duis eget ornare quam, nec lobortis nunc. Ut dui ex, faucibus ut finibus dictum, pretium in elit. Aliquam ac molestie sem. " +
                        "Morbi dictum nisl in justo venenatis, id ultricies elit porta.",
                        Image = "1.jpg",
                        Genre = "Sandbox",
                        Developer = "Mojang",
                        MinimalRequirements = new Requirement
                        {
                            CPU = "Intel Core i3 3210 / AMD A8 7600 APU",
                            DriveSize = 180,
                            GPU = "NVIDIA GeForce 400 Series / AMD Radeon HD 7000 series",
                            OS = "64-bit Windows 7",
                            RAM = 4,
                        },
                        RecommendedRequirements = new Requirement
                        {
                            CPU = "Intel Core i5 4690 / AMD A10 7800",
                            DriveSize = 4000,
                            GPU = "NVIDIA GeForce 700 Series / AMD Radeon Rx 200 Series",
                            OS = "64-bit Windows 10",
                            RAM = 8,
                        },
                        CommentThreads = new List<GameCommentThread> { new GameCommentThread {
                            Name = "Najlepsze sposoby na przetrwanie pierwszej nocy w Minecraft",
                            Comments = new List<GameComment>
                            {
                                new GameComment
                                {
                                    Author = users[0],
                                    Date = DateTime.Now,
                                    Body = "Cześć wszystkim!\r\n\r\nDziś chciałbym podzielić się moimi sprawdzonymi sposobami na przetrwanie pierwszej nocy w Minecraft. Rozumiem, że dla nowych graczy może to być naprawdę trudne, dlatego postanowiłem podzielić się kilkoma wskazówkami.\r\n\r\nPo pierwsze, zacznij od zbierania surowców, takich jak drewno, które pozwoli ci na zbudowanie narzędzi i schronienia. Następnie szybko wykop dół w ziemi i zbuduj sobie prowizoryczne schronienie, aby uniknąć potworów w nocy. Pamiętaj, że musisz być gotowy na ataki, więc warto również wykuć sobie miecz i tarczę.\r\n\r\nJeśli możecie, zbudujcie również prosty piec, aby móc przygotować jedzenie. Nie zapomnijcie zbierać surowców, takich jak węgiel, który będzie potrzebny do utrzymania ognia w piecu.\r\n\r\nMam nadzieję, że te porady pomogą wam przetrwać pierwszą noc w Minecraft. Jeśli macie jeszcze jakieś pytania, śmiało pytajcie!\r\n\r\nPowodzenia i miłej gry!\r\n\r\n"
                                },
                                new GameComment
                                {
                                    Author = users[1],
                                    Date = DateTime.Now,
                                    Body = "Cześć!\r\n\r\nDzięki za udostępnienie tych wskazówek na przetrwanie pierwszej nocy w Minecraft. To zawsze jest trudne dla nowych graczy, więc każda pomoc jest mile widziana! Mam tylko jedno pytanie: czy zalecałbyś budowanie schronienia na drzewie? Słyszałem, że to może być skuteczna metoda ochrony przed potworami. Jakie są twoje doświadczenia w tej kwestii?\r\n\r\nPozdrawiam i dziękuję jeszcze raz za udostępnienie tych porad!"
                                },
                                new GameComment
                                {
                                    Author = users[2],
                                    Date = DateTime.Now,
                                    Body = "Witam!\r\n\r\nDzięki za wskazówki dotyczące przetrwania pierwszej nocy w Minecraft. Moje pierwsze doświadczenia były dość chaotyczne, ale teraz, dzięki twoim poradom, mam nadzieję, że sobie poradzę. Mam jeszcze jedno pytanie: czy zalecałbyś szukanie jaskiń lub kopalni podczas pierwszej nocy? Słyszałem, że można tam znaleźć cenne surowce, ale nie wiem, czy to jest bezpieczne w tak wczesnym etapie gry. Czy miałeś jakieś doświadczenia w tym zakresie?\r\n\r\nDziękuję jeszcze raz i pozdrawiam!\r\n\r\n\r\n\r\n\r\n"
                                }
                            }
                        },
                        new GameCommentThread {
                            Name = "Witajcie w mojej epickiej wiosce! Zapraszam do zwiedzania!",
                            Comments = new List<GameComment>
                            {
                                new GameComment
                                {
                                    Author = users[1],
                                    Date = DateTime.Now,
                                    Body = "Hej wszystkim!\r\n\r\nDzisiaj chciałbym podzielić się z wami moim najnowszym projektem w Minecraft - epicką wioską, którą udało mi się zbudować. Spędziłem wiele godzin planując i budując różnorodne budowle, a teraz jestem dumny z efektów mojej pracy.\r\n\r\nW mojej wiosce znajdziecie imponujące zamki, kolorowe ogrody, unikalne domy i wiele innych fascynujących miejsc. Każdy szczegół został starannie zaprojektowany, aby stworzyć przyjemne i wciągające otoczenie.\r\n\r\nZapraszam was serdecznie do zwiedzenia mojej wioski! Wybierzcie się na wirtualną podróż i odkrywajcie jej zakamarki. Będę wdzięczny za wszelkie uwagi i sugestie dotyczące mojej pracy. Może też jesteście ciekawi, jakie materiały i techniki budowania zastosowałem?\r\n\r\nNie mogę się doczekać, aby podzielić się z wami moim projektem i usłyszeć wasze opinie. Mam nadzieję, że spędzicie miło czas w mojej epickiej wiosce!\r\n\r\nŻyczę wam fantastycznej przygody!"
                                }
                            }
                        } },
                    },
                    new Game
                    {
                        Name = "Rayman 3: Hoodlum Havoc",
                        ReleaseDate = new DateTime(2003, 02, 15),
                        Platform = "PC, Xbox, PlayStation",
                        Publisher = "Ubisoft",
                        Description = "Aliquam molestie mollis sagittis. Curabitur a nibh eu tortor dapibus sodales. " +
                        "Proin a aliquet turpis. Proin augue massa, posuere ac interdum at, aliquam sed sem. Donec felis turpis, viverra eget elit ut, facilisis auctor risus. " +
                        "Donec aliquam augue urna, et accumsan nisi lobortis in. Ut sagittis nunc feugiat nibh venenatis tempor. Sed rutrum ullamcorper sapien et eleifend. \n\n" +
                        "Integer et odio tincidunt, luctus felis non, eleifend libero. Etiam a vulputate enim. " +
                        "Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. " +
                        "In ac ante iaculis, scelerisque orci varius, ornare ligula. Donec nec faucibus eros. " +
                        "Phasellus lacinia elit arcu, condimentum sodales libero pretium sed. In ut commodo orci. Sed sagittis viverra ante, id dignissim metus mollis ut.",
                        Image = "2.jpg",
                        Genre = "Platformówki",
                        Developer = "Ubisoft",
                        MinimalRequirements = new Requirement
                        {
                            CPU = "Pentium III 600 MHz / AMD Athlon 600 MHz",
                            DriveSize = 800,
                            GPU = "Kompatybilna z DirectX 8.1",
                            OS = "Windows XP",
                            RAM = 0.256,
                        },
                        RecommendedRequirements = new Requirement
                        {
                            CPU = "Pentium III 1 GHz / AMD Athlon 1 Ghz",
                            DriveSize = 800,
                            GPU = "Kompatybilna z DirectX 9",
                            OS = "Windows XP",
                            RAM = 0.512,
                        }
                    },
                    new Game
                    {
                        Name = "FlatOut 2",
                        ReleaseDate = new DateTime(2006, 06, 30),
                        Platform = "PC, Xbox, PlayStation",
                        Publisher = "Empire Interactive",
                        Description = "Donec vehicula ornare elit, nec tempor ante ornare a. In hac habitasse platea dictumst. " +
                        "Etiam rhoncus ornare vestibulum. Quisque id odio pellentesque, maximus massa in, aliquam quam. " +
                        "Donec lacus sem, lobortis a dolor vel, condimentum pellentesque orci. " +
                        "Aenean nunc ipsum, cursus a blandit at, pellentesque id purus. Quisque tincidunt urna vel sem maximus, a efficitur urna suscipit.",
                        Image = "3.jpg",
                        Genre = "Wyścigi",
                        Developer = "Bugbear Entertainment",
                        MinimalRequirements = new Requirement
                        {
                            CPU = "Intel Pentium 4 2.0GHz / AMD Athlon XP 2000+",
                            DriveSize = 3500,
                            GPU = "AMD Radeon HD 4350 / NVIDIA GeForce 6600 GT",
                            OS = "32-bit Windows XP",
                            RAM = 0.256,
                        },
                        RecommendedRequirements = new Requirement
                        {
                            CPU = "Intel Pentium 4 2.0GHz / AMD Athlon XP 2500+",
                            DriveSize = 4000,
                            GPU = "AMD Radeon 9600 Series / NVIDIA GeForce 6600",
                            OS = "32-bit Windows XP",
                            RAM = 0.512,
                        }
                    }
                };

            await context.Game.AddRangeAsync(games);
        }

        public static async Task SeedNews(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            if (context.News.Any())
            {
                return;
            }

            var author = await userManager.FindByNameAsync("Editor");

            // thank you ChatGPT for these titles and content ;)
            var news = new List<News>
                {
                    new News
                    {
                        Author = author,
                        Content = "Ostatnio na rynku gier video pojawiła się nowa generacja konsol, która zaskakuje graczy swoimi innowacjami i możliwościami. Najnowsze modele, takie jak Xbox Series X, PlayStation 5 czy Nintendo Switch Pro, oferują nie tylko znacznie lepszą grafikę, ale także potężniejsze procesory i większą moc obliczeniową. Gracze mogą się spodziewać płynniejszej rozgrywki, krystalicznie czystych obrazów i bardziej realistycznego dźwięku.\r\n\r\n",
                        PublicationDate = DateTime.Now,
                        Tags = "gry, nowości, konsole",
                        ImagePath = "1.jpg",
                        Title = "Nowa generacja konsol zaskakuje graczy: Odkrywamy, co czeka nas w najnowszych modelach!"
                    },
                    new News
                    {
                        Author = author,
                        Content = "Rok 2023 okazał się niezwykle udany dla branży gier wideo, a niektóre tytuły zdobyły serca milionów graczy na całym świecie. Wśród najpopularniejszych gier znalazły się takie tytuły jak \"CyberChronicles: Awakening\", epicka przygoda science fiction z niesamowitą fabułą i rozbudowanym systemem walki, oraz \"Fantasy Quest: Legends of Magic\", pełna magii i tajemnic gra RPG, w której gracze mogą eksplorować olśniewające światy fantasy i wcielać się w postacie pełne mocy.\r\n\r\n",
                        PublicationDate = DateTime.Now,
                        Tags = "gry, nowości, ranking",
                        ImagePath = "2.jpg",
                        Title = "Czołowe gry wideo roku 2023: Które tytuły zdobyły serca milionów graczy?"
                    },
                    new News
                    {
                        Author = author,
                        Content = "Gra multiplayer \"BattleZone\" okazała się prawdziwym fenomenem na rynku e-sportu. Dzięki dynamicznej rozgrywce, wciągającym trybom rozgrywki i strategicznemu podejściu, \"BattleZone\" zdobył serca milionów graczy na całym świecie. Wirtualne turnieje i ligi, w których gracze rywalizują ze sobą, przyciągają uwagę zarówno fanów gier wideo, jak i profesjonalnych zawodników. Czy \"BattleZone\" stanie się nowym królem e-sportu?\r\n\r\n",
                        PublicationDate = DateTime.Now,
                        Tags = "gry, battlezone, esport, sukces, multiplayer",
                        ImagePath = "3.jpg",
                        Title = "Wielki sukces gry multiplayer: Jak 'BattleZone' podbił rynek e-sportu?"
                    },
                    new News
                    {
                        Author = author,
                        Content = "W świecie gier wideo przełomowe technologie wciąż zdobywają na popularności, a jedną z nich jest wirtualna rzeczywistość (VR). Dzięki specjalnym zestawom słuchawkowym i kontrolerom, gracze mogą w pełni zanurzyć się w wirtualnym świecie i doświadczyć gier w zupełnie nowy sposób. Czy VR jest przyszłością rozrywki wideo? Coraz większa liczba tytułów wprowadza wsparcie dla tej technologii, a rozwój VR wciąż trwa, obiecując jeszcze bardziej zaawansowane i emocjonujące doświadczenia dla graczy.\r\n\r\n",
                        PublicationDate = DateTime.Now,
                        Tags = "gry, nowości, technologie, vr",
                        ImagePath = "4.jpg",
                        Title = "Przełomowe technologie w świecie gier: Czy VR jest przyszłością rozrywki wideo?"
                    },
                    new News
                    {
                        Author = author,
                        Content = "Studio XYZ, znane z tworzenia gier wysokiej jakości, właśnie ogłosiło premierę swojego najnowszego hitu. Ta oczekiwana przez fanów produkcja obiecuje epicką przygodę, wciągającą fabułę i niesamowitą grafikę. Gracze mogą spodziewać się olśniewających światów do eksploracji, emocjonujących misji i niezapomnianych postaci. Premiera jest już tuż za rogiem, więc przygotujcie się na niezapomnianą podróż!\r\n\r\n",
                        PublicationDate = DateTime.Now,
                        Tags = "gry, nowości, xyz",
                        ImagePath = "5.jpg",
                        Title = "Studio XYZ ogłasza premierę swojego najnowszego hitu: Przygotujcie się na wielką przygodę!"
                    },
                    new News
                    {
                        Author = author,
                        Content = "Ostatnio w świecie gier wideo pojawiła się debata na temat roli kontekstu historycznego w tworzeniu gier. Czy twórcy powinni uwzględniać znaczenie i wydarzenia historyczne, kiedy projektują swoje gry? Niektórzy twierdzą, że kontekst historyczny może być istotny dla autentyczności i głębi narracji, podczas gdy inni uważają, że gry powinny być całkowicie wolne od ograniczeń historycznych. Kontrowersje te stwarzają wiele pytań i wątpliwości dotyczących odpowiedzialności twórców gier wideo.\r\n\r\n",
                        PublicationDate = DateTime.Now,
                        Tags = "gry, kontrowersje, historia",
                        ImagePath = "6.jpg",
                        Title = "Kontrowersje w świecie gier: Czy kontekst historyczny powinien mieć znaczenie dla twórców?"
                    },

                };

            await context.News.AddRangeAsync(news);
        }
    }
}