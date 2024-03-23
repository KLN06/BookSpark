using BookSpark.Data.Entities;
using Humanizer;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Mysqlx.Crud;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using System.Collections.Generic;
using System.Diagnostics;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.Intrinsics.X86;
using System.Security.Policy;

namespace BookSpark.Data
{
    public class Seed
    {

        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Genres.Any())
                {
                    context.Genres.AddRange(new List<Genre>()
                    {
                        new Genre()
                        {
                            Name = "Science fiction"
                        },
                        new Genre()
                        {
                            Name = "Horror"
                        },
                        new Genre()
                        {
                            Name = "Thriller"
                        },
                        new Genre()
                        {
                            Name = "Romance"
                        },
                        new Genre()
                        {
                            Name = "Mystery"
                        },
                        new Genre()
                        {
                            Name = "Children literature"
                        },
                        new Genre()
                        {
                            Name = "Adventure"
                        }
                    });
                    context.SaveChanges();
                }
                //Authors
                if (!context.Authors.Any())
                {

                    context.Authors.AddRange(new List<Author>()
                    {
                        new Author()
                        {
                            Name = "Arthur C. Clarke",
                            Birthdate = new DateTime(1917, 12, 16, 0, 0, 0),
                            Biography = "Sir Arthur Charles Clarke CBE FRAS was an English science fiction writer, science writer, futurist, inventor, undersea explorer, and television series host. He co-wrote the screenplay for the 1968 film 2001: A Space Odyssey, widely regarded as one of the most influential films of all time.",
                            Books = new List<Book>()
                        },
                        new Author()
                        {
                            Name = "Stephen King",
                            Birthdate = new DateTime(1947, 09, 21, 0, 0, 0),
                            Biography = "Stephen Edwin King is an American author. Called the \"King of Horror\", he has also explored other genres, among them suspense, crime, science-fiction, fantasy and mystery. He has also written approximately 200 short stories, most of which have been published in collections.",
                            Books = new List<Book>()
                        },
                        new Author()
                        {
                            Name = "Victor Methos",
                            Birthdate = new DateTime(1975, 07, 09, 0, 0, 0),
                            Biography = "Victor Methos is a former prosecutor specializing in violent crime and is currently a criminal defense attorney in the United States. He is the author of twenty bestselling books including THE WHITE ANGEL MURDER and SUPERHERO, both Kindle Top 100 smash hits.",
                            Books = new List<Book>()
                        },
                        new Author()
                        {
                            Name = "Chris Carter",
                            Birthdate = new DateTime(1965, 07, 14, 0, 0, 0),
                            Biography = "Chris Carter is a top bestselling author in the United Kingdom, whose books include An Evil Mind, One By One, The Death Sculptor, The Night Stalker, The Executioner and The Crucifix Killer.",
                            Books = new List<Book>()
                        },
                        new Author()
                        {
                            Name = "Anna Todd",
                            Birthdate = new DateTime(1989, 03, 20, 0, 0, 0),
                            Biography = "Anna Renee Todd is an American author, film producer, and screenwriter. She is best known for writing the book series After, which she started publishing on the social storytelling platform Wattpad. The print edition of the series was published by Gallery Books, and has been translated into several languages.",
                            Books = new List<Book>()
                        },
                        new Author()
                        {
                            Name = "Colleen Hoover",
                            Birthdate = new DateTime(1979, 12, 11, 0, 0, 0),
                            Biography = "Colleen Hoover is an American author who primarily writes novels in the romance and young adult fiction genres. She is best known for her 2016 romance novel It Ends with Us. Many of her works were self-published before being picked up by a publishing house.",
                            Books = new List<Book>()
                        },
                        new Author()
                        {
                            Name = "Agatha Christie",
                            Birthdate = new DateTime(1890, 09, 15, 0, 0, 0),
                            Biography = "Dame Agatha Mary Clarissa Christie, Lady Mallowan, DBE was an English writer known for her 66 detective novels and 14 short story collections, particularly those revolving around fictional detectives Hercule Poirot and Miss Marple.",
                            Books = new List<Book>()
                        },
                        new Author()
                        {
                            Name = "John Grisham",
                            Birthdate = new DateTime(1955, 02, 08, 0, 0, 0),
                            Biography = "John Ray Grisham Jr. is an American novelist, lawyer, and former member of the Mississippi House of Representatives, known for his best-selling legal thrillers.",
                            Books = new List<Book>()
                        },
                        new Author()
                        {
                            Name = "Roald Dahl",
                            Birthdate = new DateTime(1916, 09, 13, 0, 0, 0),
                            Biography = "Roald Dahl was a British author of popular children's literature and short stories, a poet, screenwriter and a wartime fighter ace. His books have sold more than 300 million copies worldwide. Dahl has been called \"one of the greatest storytellers for children of the 20th century\".",
                            Books = new List<Book>()
                        },
                        new Author()
                        {
                            Name = "Astrid Lindgren",
                            Birthdate = new DateTime(1907, 11, 14, 0, 0, 0),
                            Biography = "Astrid Anna Emilia Lindgren was born on the 14th of November 1907 on the farm Näs outside Vimmerby, in the county of Småland, and died on the 28th January 2002 in her home on Dalagatan 46 in Stockholm. She wrote 34 chapter books and 41 picture books, that all together have sold a staggering 170 million copies and been translated into more than 100 languages.",
                            Books = new List<Book>()
                        },
                        new Author()
                        {
                            Name = "Lewis Carroll",
                            Birthdate = new DateTime(1832, 01, 27, 0, 0, 0),
                            Biography = "Charles Lutwidge Dodgson, better known by his pen name Lewis Carroll, was an English author, poet, mathematician and photographer. His most notable works are Alice's Adventures in Wonderland and its sequel Through the Looking-Glass. He was noted for his facility with word play, logic, and fantasy.",
                            Books = new List<Book>()
                        },
                        new Author()
                        {
                            Name = "Jules Verne",
                            Birthdate = new DateTime(1828, 02, 08, 0, 0, 0),
                            Biography = "Jules Gabriel Verne was a French novelist, poet, and playwright. His collaboration with the publisher Pierre-Jules Hetzel led to the creation of the Voyages extraordinaires, a series of bestselling adventure novels including Journey to the Center of the Earth (1864), Twenty Thousand Leagues Under the Seas (1870), and Around the World in Eighty Days (1872). His novels, always well documented, are generally set in the second half of the 19th century, taking into account the technological advances of the time.",
                            Books = new List<Book>()
                        }

                    });
                    context.SaveChanges();
                }

                //Books
                if (!context.Books.Any())
                {

                    context.Books.AddRange(new List<Book>()
                    {
                        new Book()
                        {
                            Title = "2001: A Space Odyssey",
                            Description = "The classic science fiction novel that captures and expands on the vision of Stanley Kubrick’s immortal film—and changed the way we look at the stars and ourselves. From the savannas of Africa at the dawn of mankind to the rings of Saturn as man ventures to the outer rim of our solar system, 2001: A Space Odyssey is a journey unlike any other.\r\n\r\nThis allegory about humanity’s exploration of the universe—and the universe’s reaction to humanity—is a hallmark achievement in storytelling that follows the crew of the spacecraft Discovery as they embark on a mission to Saturn. Their vessel is controlled by HAL 9000, an artificially intelligent supercomputer capable of the highest level of cognitive functioning that rivals—and perhaps threatens—the human mind.\r\n\r\nGrappling with space exploration, the perils of technology, and the limits of human power, 2001: A Space Odyssey continues to be an enduring classic of cinematic scope.",
                            PublishedYear = 1968,
                            GenreId = 1,
                            AuthorId = 1,
                            ImageLink = "https://debarbasyboinas.files.wordpress.com/2016/03/2001_pk_remix_book_by_marcosrstone.jpg"
                        },
                        new Book()
                        {
                            Title = "It",
                            Description = "A promise made twenty-eight years ago calls seven adults to reunite in Derry, Maine, where as teenagers they battled an evil creature that preyed on the city's children. Unsure that their Losers Club had vanquished the creature all those years ago, the seven had vowed to return to Derry if IT should ever reappear. Now, children are being murdered again and their repressed memories of that summer return as they prepare to do battle with the monster lurking in Derry's sewers once more.",
                            PublishedYear = 1986,
                            GenreId = 2,
                            AuthorId = 2,
                            ImageLink = "https://d28hgpri8am2if.cloudfront.net/book_images/onix/cvr9781982127794/it-9781982127794_hr.jpg"
                        },
                        new Book()
                        {
                            Title = "Carrie",
                            Description = "A modern classic, Carrie introduced a distinctive new voice in American fiction -- Stephen King. The story of misunderstood high school girl Carrie White, her extraordinary telekinetic powers, and her violent rampage of revenge, remains one of the most barrier-breaking and shocking novels of all time.\r\n\r\nMake a date with terror and live the nightmare that is...Carrie",
                            PublishedYear = 1974,
                            GenreId = 2,
                            AuthorId = 2,
                            ImageLink = "https://m.media-amazon.com/images/I/71ifzjx0reL._AC_UF1000,1000_QL80_.jpg"
                        },
                        new Book()
                        {
                            Title = "The Shining",
                            Description = "Jack Torrance's new job at the Overlook Hotel is the perfect chance for a fresh start. As the off-season caretaker at the atmospheric old hotel, he'll have plenty of time to spend reconnecting with his family and working on his writing. But as the harsh winter weather sets in, the idyllic location feels ever more remote...and more sinister. And the only one to notice the strange and terrible forces gathering around the Overlook is Danny Torrance, a uniquely gifted five-year-old.",
                            PublishedYear = 1977,
                            GenreId = 2,
                            AuthorId = 2,
                            ImageLink = "https://m.media-amazon.com/images/I/81zqohMOk-L._AC_UF894,1000_QL80_.jpg"
                        },
                        new Book()
                        {
                            Title = "A Killer's Wife",
                            Description = "From the bestselling author of The Neon Lawyer comes a gripping thriller about a prosecutor confronted with the darkest part of her past and the worst fears for her future… Fourteen years ago, prosecutor Jessica Yardley’s husband went to prison for a series of brutal murders. She’s finally created a life with her daughter and is a well-respected attorney. She’s moving on. But when a new rash of homicides has her ex-husband, Eddie, written all over them—the nightmares of her past come back to life.\r\n\r\nThe FBI asks Jessica to get involved in the hunt for this copycat killer—which means visiting her ex and collaborating with the man who tore her life apart.\r\n\r\nAs the copycat’s motives become clearer, the new life Jessica created for herself gets darker. She must ask herself who she can trust and if she’s capable of stopping the killer—a man whose every crime is a bloody valentine from a twisted mastermind she’s afraid she may never escape.",
                            PublishedYear = 2020,
                            GenreId = 3,
                            AuthorId = 3,
                            ImageLink = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1578494269i/46224901.jpg"
                        },
                        new Book()
                        {
                            Title = "Crimson Lake Road",
                            Description = "Bestselling author Victor Methos’s acclaimed series continues as prosecutor Jessica Yardley races to catch an art-obsessed serial killer before she becomes his next masterpiece. Retiring prosecutor Jessica Yardley can’t turn down one last investigation. This time, it’s a set of murders inspired by a series of grisly paintings called The Night Things. She’s the only one who can catch the killer, who’s left a trail of bodies in a rural community outside of Las Vegas. But the more Jessica finds out, the less clear her case becomes.Out of options, she’s forced to consult her serial killer ex-husband—to gain additional insight into the crimes and the killer’s motivations. By the time Jessica realizes that pursuing this case is a deadly mistake, it’s too late to turn back. Can she catch the killer, or will she be the final addition to a killer’s masterpiece?",
                            PublishedYear = 2021,
                            GenreId = 3,
                            AuthorId = 3,
                            ImageLink = "https://m.media-amazon.com/images/I/51Bz6ByeJ6L.jpg"
                        },
                        new Book()
                        {
                            Title = "The Crucifix Killer",
                            Description = "When the body of a young woman is discovered in a derelict cottage in the middle of Los Angeles National Forest, Homicide Detective Robert Hunter finds himself entering a horrific and recurring nightmare. Naked, strung from two parallel wooden posts, the victim was sadistically tortured before meeting an excruciatingly painful death.\r\n\r\nAll the skin has been ripped from her face - while she was still alive. On the nape of her neck has been carved a strange double-cross: the signature of a psychopath known as the Crucifix Killer. But that's impossible. Because two years ago, the Crucifix Killer was caught and executed. Could this therefore be a copycat killer? Or could the unthinkable be true?\r\n\r\nIs the real killer still out there, ready to embark once again on a vicious and violent killing spree, selecting his victims seemingly at random, taunting Robert Hunter with his inability to catch him? Hunter and his rookie partner are about to enter a nightmare beyond imagining.",
                            PublishedYear = 2009,
                            GenreId = 3,
                            AuthorId = 4,
                            ImageLink = "https://m.media-amazon.com/images/I/81nhXst5wCL._AC_UF350,350_QL50_.jpg"
                        },
                        new Book()
                        {
                            Title = "One by One",
                            Description = "'I need your help, Detective. Fire or water?' Detective Robert Hunter of the LAPD's Homicide Special Section receives an anonymous call asking him to go to a specific web address - a private broadcast. Hunter logs on and a show devised for his eyes only immediately begins. But the caller doesn't want Detective Hunter to just watch, he wants him to participate, and refusal is simply not an option. Forced to make a sickening choice, Hunter must sit and watch as an unidentified victim is tortured and murdered live over the Internet. The LAPD, together with the FBI, use everything at their disposal to electronically trace the transmission down, but this killer is no amateur, and he has covered his tracks from start to finish. And before Hunter and his partner Garcia are even able to get their investigation going, Hunter receives a new phone call. A new website address. A new victim. But this time the killer has upgraded his game into a live murder reality show, where anyone can cast the deciding vote.",
                            PublishedYear = 2013,
                            GenreId = 3,
                            AuthorId = 4,
                            ImageLink = "https://m.media-amazon.com/images/I/71AAee3zxdL._AC_UF1000,1000_QL80_.jpg"
                        },
                        new Book()
                        {
                            Title = "After",
                            Description = "Tessa is a good girl with a sweet, reliable boyfriend back home. She’s got direction, ambition, and a mother who’s intent on keeping her that way. But she’s barely moved into her freshman dorm when she runs into Hardin. With his tousled brown hair, cocky British accent, tattoos, and lip ring, Hardin is cute and different from what she’s used to.\r\n\r\nBut he’s also rude—to the point of cruelty, even. For all his attitude, Tessa should hate Hardin. And she does—until she finds herself alone with him in his room. Something about his dark mood grabs her, and when they kiss it ignites within her a passion she’s never known before.\r\n\r\nHe’ll call her beautiful, then insist he isn't the one for her and disappear again and again. Despite the reckless way he treats her, Tessa is compelled to dig deeper and find the real Hardin beneath all his lies. He pushes her away again and again, yet every time she pushes back, he only pulls her in deeper.Tessa already has the perfect boyfriend. So why is she trying so hard to overcome her own hurt pride and Hardin’s prejudice about nice girls like her?\r\n\r\nUnless…could this be love?\r\n\r\nNow newly revised and expanded, Anna Todd’s After fanfiction racked up 1 billion reads online and captivated readers across the globe. Experience the Internet’s most talked-about book for yourself!\r\n\r\nThere was the time before Tessa met Hardin, and then there’s everything AFTER ... Life will never be the same.",
                            PublishedYear = 2014,
                            GenreId = 4,
                            AuthorId = 5,
                            ImageLink = "https://m.media-amazon.com/images/I/71HA0LMmwZL._AC_UF1000,1000_QL80_.jpg"
                        },
                        new Book()
                        {
                            Title = "It Ends with Us",
                            Description = "Sometimes it is the one who loves you who hurts you the most. Lily hasn’t always had it easy, but that’s never stopped her from working hard for the life she wants. She’s come a long way from the small town in Maine where she grew up — she graduated from college, moved to Boston, and started her own business. So when she feels a spark with a gorgeous neurosurgeon named Ryle Kincaid, everything in Lily’s life suddenly seems almost too good to be true.\r\n\r\nRyle is assertive, stubborn, maybe even a little arrogant. He’s also sensitive, brilliant, and has a total soft spot for Lily. And the way he looks in scrubs certainly doesn’t hurt. Lily can’t get him out of her head. But Ryle’s complete aversion to relationships is disturbing. Even as Lily finds herself becoming the exception to his “no dating” rule, she can’t help but wonder what made him that way in the first place. As questions about her new relationship overwhelm her, so do thoughts of Atlas Corrigan — her first love and a link to the past she left behind. He was her kindred spirit, her protector. When Atlas suddenly reappears, everything Lily has built with Ryle is threatened.",
                            PublishedYear = 2016,
                            GenreId = 4,
                            AuthorId = 6,
                            ImageLink = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1688011813i/27362503.jpg"
                        },
                        new Book()
                        {
                            Title = "November 9",
                            Description = "Beloved #1 New York Times bestselling author Colleen Hoover returns with an unforgettable love story between a writer and his unexpected muse.\r\n\r\nFallon meets Ben, an aspiring novelist, the day before her scheduled cross-country move. Their untimely attraction leads them to spend Fallon’s last day in L.A. together, and her eventful life becomes the creative inspiration Ben has always sought for his novel. Over time and amidst the various relationships and tribulations of their own separate lives, they continue to meet on the same date every year. Until one day Fallon becomes unsure if Ben has been telling her the truth or fabricating a perfect reality for the sake of the ultimate plot twist.\r\n\r\nCan Ben’s relationship with Fallon—and simultaneously his novel—be considered a love story if it ends in heartbreak?",
                            PublishedYear = 2015,
                            GenreId = 4,
                            AuthorId = 6,
                            ImageLink = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1447138036i/25111004.jpg"
                        },
                        new Book()
                        {
                            Title = "And Then There Were None",
                            Description = "First, there were ten—a curious assortment of strangers summoned as weekend guests to a little private island off the coast of Devon. Their host, an eccentric millionaire unknown to all of them, is nowhere to be found. All that the guests have in common is a wicked past they're unwilling to reveal—and a secret that will seal their fate. For each has been marked for murder. A famous nursery rhyme is framed and hung in every room of the mansion:\r\n\r\n\"Ten little boys went out to dine; One choked his little self and then there were nine. Nine little boys sat up very late; One overslept himself and then there were eight. Eight little boys traveling in Devon; One said he'd stay there then there were seven. Seven little boys chopping up sticks; One chopped himself in half and then there were six. Six little boys playing with a hive; A bumblebee stung one and then there were five. Five little boys going in for law; One got in Chancery and then there were four. Four little boys going out to sea; A red herring swallowed one and then there were three. Three little boys walking in the zoo; A big bear hugged one and then there were two. Two little boys sitting in the sun; One got frizzled up and then there was one. One little boy left all alone; He went out and hanged himself and then there were none.\"\r\n\r\nWhen they realize that murders are occurring as described in the rhyme, terror mounts. One by one they fall prey. Before the weekend is out, there will be none. Who has choreographed this dastardly scheme? And who will be left to tell the tale? Only the dead are above suspicion.",
                            PublishedYear = 1939,
                            GenreId = 5,
                            AuthorId = 7,
                            ImageLink = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1638425885i/16299.jpg"
                        },
                        new Book()
                        {
                            Title = "Murder on the Orient Express",
                            Description = "Just after midnight, a snowdrift stops the famous Orient Express in its tracks as it travels through the mountainous Balkans. The luxurious train is surprisingly full for the time of the year but, by the morning, it is one passenger fewer. An American tycoon lies dead in his compartment, stabbed a dozen times, his door locked from the inside.\r\n\r\nOne of the passengers is none other than detective Hercule Poirot. On vacation.\r\n\r\nIsolated and with a killer on board, Poirot must identify the murderer—in case he or she decides to strike again.",
                            PublishedYear = 1934,
                            GenreId = 5,
                            AuthorId = 7,
                            ImageLink = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1486131451i/853510.jpg"
                        },
                        new Book()
                        {
                            Title = "Death on the Nile",
                            Description = "The tranquility of a lovely cruise along the Nile is shattered by the discovery that Linnet Ridgeway has been shot. She was young, stylish and beautiful, a girl who had everything – until she lost her life.\r\n\r\nWho is also on board? Christie's great detective Hercule Poirot is on holiday. He recalls an earlier outburst by a fellow passenger: ‘I’d like to put my dear little pistol against her head and just press the trigger.’ Despite the exotic setting, nothing is ever quite what it seems…",
                            PublishedYear = 1937,
                            GenreId = 5,
                            AuthorId = 7,
                            ImageLink = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1486837149i/131359.jpg"
                        },
                        new Book()
                        {
                            Title = "The Client",
                            Description = "In a weedy clearing on the outskirts of Memphis, two boys watch a shiny Lincoln pull up to the curb...Eleven-year-old Mark Sway and his younger brother were sharing a forbidden cigarette when a chance encounter with a suicidal lawyer left Mark knowing a bloody and explosive secret: the whereabouts of the most sought-after dead body in America. Now Mark is caught between a legal system gone mad and a mob killer desperate to cover up his crime. And his only ally is a woman named Reggie Love, who has been a lawyer for all of four years. Prosecutors are willing to break all the rules to make Mark talk. The mob will stop at nothing to keep him quiet. And Reggie will do anything to protect her client --even take a last, desperate gamble that could win Mark his freedom... or cost them both their lives.",
                            PublishedYear = 1993,
                            GenreId = 5,
                            AuthorId = 8,
                            ImageLink = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1554192326i/5359.jpg"
                        },
                        new Book()
                        {
                            Title = "Matilda",
                            Description = "“The Trunchbull” is no match for Matilda!\r\n\r\nMatilda is a little girl who is far too good to be true. At age five-and-a-half she's knocking off double-digit multiplication problems and blitz-reading Dickens. Even more remarkably, her classmates love her even though she's a super-nerd and the teacher's pet. But everything is not perfect in Matilda's world...\r\n\r\nFor starters she has two of the most idiotic, self-centered parents who ever lived. Then there's the large, busty nightmare of a school principal, Miss (\"The\") Trunchbull, a former hammer-throwing champion who flings children at will, and is approximately as sympathetic as a bulldozer. Fortunately for Matilda, she has the inner resources to deal with such annoyances: astonishing intelligence, saintly patience, and an innate predilection for revenge.\r\n\r\nRoald Dahl was a spy, ace fighter-pilot, chocolate historian, and medical inventor. He was also the author of Charlie and the Chocolate Factory, Matilda, The BFG, and many more brilliant stories. He remains the World's No. 1 Storyteller.",
                            PublishedYear = 1988,
                            GenreId = 6,
                            AuthorId = 9,
                            ImageLink = "https://m.media-amazon.com/images/I/91lsLvNSccL.jpg"
                        },
                        new Book()
                        {
                            Title = "Charlie and the Chocolate Factory",
                            Description = "Augustus Gloop eats himself sick.\r\n\r\nVeruca Salt is a spoiled rotten brat.\r\n\r\nViolet Beauregarde chews gum day and night.\r\n\r\nMike Teavee is a television fiend.\r\n\r\nCharlie Buckett, Our Hero, is brave and true and very, very hungry.\r\n\r\nWhat do these five have in common? Why, they're the luckiest children in the entire world; they've each won the chance to enter Willy Wonka's famous, mysterious chocolate factory.\r\n\r\nWhat happens when the big doors swing open to reveal Mr. Wonka's secrets? What happens when they come upon the tiny factory workers who sing in rhyme? What happens when, one by one, the children disobey Mr. Wonka's orders? In Roald Dahl's most popular story for children, the nasty are punished and the good are deliciously, sumptuously rewarded.\r\n(front flap)",
                            PublishedYear = 1964,
                            GenreId = 6,
                            AuthorId = 9,
                            ImageLink = "https://m.media-amazon.com/images/I/81Dp5Of3zeL._AC_UF1000,1000_QL80_.jpg"
                        },
                        new Book()
                        {
                            Title = "Pippi Longstocking",
                            Description = "Tommy and his sister Annika have a new neighbor, and her name is Pippi Longstocking. She has crazy red pigtails, no parents to tell her what to do, a horse that lives on her porch, and a flair for the outrageous that seems to lead to one adventure after another!",
                            PublishedYear = 1945,
                            GenreId = 6,
                            AuthorId = 10,
                            ImageLink = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1519300455i/19302.jpg"
                        },
                        new Book()
                        {
                            Title = "Karlsson on the Roof",
                            Description = "Imagine Smidge's delight when, one day, a little man with a propeller on his back appears hovering at the window! It's Karlson and he lives in a house on the roof. Soon Smidge and Karlson are sharing all sorts of adventures, from tackling thieves and playing tricks to looping the loop and running across the rooftops. Fun and chaos burst from these charming, classic stories.",
                            PublishedYear = 1955,
                            GenreId = 6,
                            AuthorId = 10,
                            ImageLink = "https://m.media-amazon.com/images/I/71gXnspdijL._AC_UF1000,1000_QL80_.jpg"
                        },
                        new Book()
                        {
                            Title = "Alice in Wonderland",
                            Description = "Chris Riddell's brilliant new full-colour illustrated Alice’s Adventures in Wonderland in a sumptuous hardback and jacketed edition. A perfect gift for families, children and all fans of this much-loved favourite classic.\r\n\r\nFirst published by Macmillan more than 150 years ago, Lewis Carroll’s iconic story has been loved and enjoyed by generations of children.\r\n\r\nThis edition presents Lewis Carroll's complete text, with new illustrations from Costa Award- and Kate Greenaway Medal-winner Chris Riddell. Published 200 years after the birth of Alice’s first illustrator, Sir John Tenniel, also the political cartoonist of his time, Chris Riddell's illustrations set a new bar in terms of excellence with his unique, rich and evocative interpretation of Carroll's world.\r\n\r\nWith the curious, quick-witted Alice at its heart, readers will not only rediscover characters such as the charming White Rabbit, the formidable Queen of Hearts, the Mad Hatter and the grinning Cheshire Cat but will find fresh and wonderful creations of these characters by a true master of his art,; images that will live in our hearts and minds for generations to come.",
                            PublishedYear = 1865,
                            GenreId = 7,
                            AuthorId = 11,
                            ImageLink = "https://images.penguinrandomhouse.com/cover/9780147515872"
                        },
                        new Book()
                        {
                            Title = "Alice Through the Looking Glass",
                            Description = "In 1865, English author CHARLES LUTWIDGE DODGSON (1832-1898), aka Lewis Carroll, wrote a fantastical adventure story for the young daughters of a friend. The adventures of Alice-named for one of the little girls to whom the book was dedicated-who journeys down a rabbit hole and into a whimsical underworld realm instantly struck a chord with the British public, and then with readers around the world. In 1872, in reaction to the universal acclaim *Alice's Adventures in Wonderland* received, Dodgson published this sequel. Nothing is quite what it seems once Alice journeys through the looking-glass, and Dodgson's wit is infectious as he explores concepts of mirror imagery, time running backward, and strategies of chess-all wrapped up in the exploits of a spirited young girl who parries with the Red Queen, Tweedledee and Tweedledum, and other unlikely characters. In many ways, this sequel has had an even greater impact on today's pop culture than the first book.",
                            PublishedYear = 1871,
                            GenreId = 7,
                            AuthorId = 11,
                            ImageLink = "https://d28hgpri8am2if.cloudfront.net/book_images/onix/cvr9781665925808/through-the-looking-glass-9781665925808_hr.jpg"
                        },

                    });
                    context.SaveChanges();
                }
            }
        }

    }
}
