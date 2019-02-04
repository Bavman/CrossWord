using CrossWord.GameTemplates;
using System.Collections.Generic;

namespace CrossWord.Game
{
    public class WordList
    {
        #region Singleton Setup
        private WordList()
        {

        }

        static WordList _instance;

        public static WordList Instance()
        {
            if (_instance == null)
            {

                _instance = new WordList();

            }

            return _instance;
        }
        #endregion

        public List<WordAndDefinition> WordAndDefinitions = new List<WordAndDefinition>
        {
            new WordAndDefinition { Word = "FRANKLY", Definition = "\"_______, my dear, I don't give a damn.\" Gone With the Wind, 1939" },
            new WordAndDefinition { Word = "OFFER", Definition = "\"I'm going to make him an _____ he can't refuse.\" The Godfather, 1972" },
            new WordAndDefinition { Word = "CONTENDER", Definition = "\"You don't understand! I could a had class. I coulda been a _________. I could've been somebody, instead of a bum, which is what I am.\" On the Waterfront, 1954" },
            new WordAndDefinition { Word = "KANSAS", Definition = "\"Toto, I've got a feeling we're not in ______ anymore.\" The Wizard of Oz, 1939" },
            new WordAndDefinition { Word = "KID", Definition = "\"Here's looking at you, ___.\" Casablanca, 1942" },
            new WordAndDefinition { Word = "DAY", Definition = "\"Go ahead, make my ___.\" Sudden Impact, 1983" },
            new WordAndDefinition { Word = "CLOSE", Definition = "\"All right, Mr. DeMille, I'm ready for my _____-up.\" Sunset Blvd., 1950Han Solo saying may the force be with you" },
            new WordAndDefinition { Word = "FORCE", Definition = "\"May the _____ be with you.\" Star Wars, 1977" },
            new WordAndDefinition { Word = "SEATBELTS", Definition = "\"Fasten your _________. It's going to be a bumpy night.\" All About Eve, 1950" },
            new WordAndDefinition { Word = "TALKING", Definition = "\"You _______ to me?\" Taxi Driver, 1976" },
            new WordAndDefinition { Word = "COMMUNICATE", Definition = "\"What we've got here is failure to ___________.\" Cool Hand Luke, 1967" },
            new WordAndDefinition { Word = "NAPALM", Definition = "\"I love the smell of ______ in the morning.\" Apocalypse Now, 1979" },
            new WordAndDefinition { Word = "SORRY", Definition = "\"Love means never having to say you're _____.\" Love Story, 1970" },
            new WordAndDefinition { Word = "DREAMS", Definition = "\"The stuff that ______ are made of.\" The Maltese Falcon, 1941" },
            new WordAndDefinition { Word = "PHONE", Definition = "\"E.T. _____ home.\" E.T. The Extra-Terrestrial, 1982" },
            new WordAndDefinition { Word = "TIBBS", Definition = "\"They call me Mister ____!\" In the Heat of the Night, 1967" },
            new WordAndDefinition { Word = "WORLD", Definition = "\"Made it, Ma! Top of the _____!\" White Heat, 1949" },
            new WordAndDefinition { Word = "ANYMORE", Definition = "\"I'm as mad as hell, and I'm not going to take this ________!\" Network, 1976" },
            new WordAndDefinition { Word = "BEAUTIFUL", Definition = "\"Louis, I think this is the beginning of a _________ friendship.\" Casablanca, 1942" },
            new WordAndDefinition { Word = "CHIANTI", Definition = "\"A census taker once tried to test me. I ate his liver with some fava beans and a nice _______.\" The Silence of the Lambs, 1991" },
            new WordAndDefinition { Word = "BOND", Definition = "\"Bond. James ____.\" Dr. No, 1962" },
            new WordAndDefinition { Word = "HOME", Definition = "\"There's no place like ____.\" The Wizard of Oz, 1939" },
            new WordAndDefinition { Word = "PICTURES", Definition = "\"I am big! It's the ________ that got small.\" Sunset Blvd., 1950" },
            new WordAndDefinition { Word = "MONEY", Definition = "\"Show me the _____!\" Jerry Maguire, 1996" },
            new WordAndDefinition { Word = "SEE", Definition = "\"Why don't you come up sometime and ___ me?\" She Done Him Wrong, 1933" },
            new WordAndDefinition { Word = "WALKING", Definition = "\"I'm _______ here! I'm walking here!\" Midnight Cowboy, 1969" },
            new WordAndDefinition { Word = "TIME", Definition = "\"Play it, Sam. Play 'As ____ Goes By.'\" Casablanca, 1942" },
            new WordAndDefinition { Word = "HANDLE", Definition = "\"You can't ______ the truth!\" A Few Good Men, 1992" },
            new WordAndDefinition { Word = "ALONE", Definition = "\"I want to be _____.\" Grand Hotel, 1932" },
            new WordAndDefinition { Word = "ANOTHER", Definition = "\"After all, tomorrow is _______ day!\" Gone With the Wind, 1939" },
            new WordAndDefinition { Word = "USUAL", Definition = "\"Round up the _____ suspects.\" Casablanca, 1942" },
            new WordAndDefinition { Word = "HAVING", Definition = "\"I'll have what she's ______.\" When Harry Met Sally, 1989" },
            new WordAndDefinition { Word = "WHISTLE", Definition = "\"You know how to _______, don't you, Steve? You just put your lips together and blow.\" To Have and Have Not, 1944Jaws, you're gonna need a bigger boat" },
            new WordAndDefinition { Word = "BOAT", Definition = "\"You're gonna need a bigger ____.\" Jaws, 1975" },
            new WordAndDefinition { Word = "BADGES", Definition = "\"______? We ain't got no badges! We don't need no badges! I don't have to show you any stinking badges!\" The Treasure of the Sierra Madre, 1948" },
            new WordAndDefinition { Word = "BACK", Definition = "\"I'll be ___.\" The Terminator, 1984" },
            new WordAndDefinition { Word = "MAN", Definition = "\"Today, I consider myself the luckiest ___ on the face of the earth.\" The Pride of the Yankees, 1942" },
            new WordAndDefinition { Word = "BUILD", Definition = "\"If you _____ it, he will come.\" Field of Dreams, 1989" },
            new WordAndDefinition { Word = "CHOCOLATES", Definition = "\"Mama always said life was like a box of _________. You never know what you're gonna get.\" Forrest Gump, 1994" },
            new WordAndDefinition { Word = "ROB", Definition = "\"We ___ banks.\" Bonnie and Clyde, 1967" },
            new WordAndDefinition { Word = "PARIS", Definition = "\"We'll always have _____.\" Casablanca, 1942" },
            new WordAndDefinition { Word = "DEAD", Definition = "\"I see ____ people.\" The Sixth Sense, 1999" },
            new WordAndDefinition { Word = "STELLA", Definition = "\"______! Hey, ______!\" A Streetcar Named Desire, 1951" },
            new WordAndDefinition { Word = "MOON", Definition = "\"Oh, Jerry, don't let's ask for the ____. We have the stars.\" Now, Voyager, 1942" },
            new WordAndDefinition { Word = "BACK", Definition = "\"Shane. Shane. Come ____!\" Shane, 1953" },
            new WordAndDefinition { Word = "PERFECT", Definition = "\"Well, nobody's _______.\" Some Like It Hot, 1959" },
            new WordAndDefinition { Word = "ALIVE", Definition = "\"It's _____! It's _____!\" Frankenstein, 1931" },
            new WordAndDefinition { Word = "HOUSTON", Definition = "\"_______, we have a problem.\" Apollo 13, 1995" },
            new WordAndDefinition { Word = "LUCKY", Definition = "\"You've got to ask yourself one question: 'Do I feel _____?' Well, do ya, punk?\" Dirty Harry, 1971" },
            new WordAndDefinition { Word = "HELLO", Definition = "\"You had me at ‘_____.’\" Jerry Maguire, 1996" },
            new WordAndDefinition { Word = "ELEPHANT", Definition = "\"One morning I shot an ________ in my pajamas. How he got in my pajamas, I don't know.\" Animal Crackers, 1930league of their own, theres no crying in baseball" },
            new WordAndDefinition { Word = "CRYING", Definition = "\"There's no ______ in baseball!\" A League of Their Own, 1992" },
            new WordAndDefinition { Word = "DEE", Definition = "\"La-___-da, la-___-da.\" Annie Hall, 1977" },
            new WordAndDefinition { Word = "MOTHER", Definition = "\"A boy's best friend is his ______.\" Psycho, 1960" },
            new WordAndDefinition { Word = "GOOD", Definition = "\"Greed, for lack of a better word, is ____.\" Wall Street, 1987" },
            new WordAndDefinition { Word = "FRIENDS", Definition = "\"Keep your _______ close, but your enemies closer.\" The Godfather II, 1974" },
            new WordAndDefinition { Word = "GOD", Definition = "\"As ___ is my witness, I'll never be hungry again.\" Gone With the Wind, 1939" },
            new WordAndDefinition { Word = "MESS", Definition = "\"Well, here's another nice ____ you've gotten me into!\" Sons of the Desert, 1933Scarface say hello to my little friend scene" },
            new WordAndDefinition { Word = "FRIEND", Definition = "\"Say \"hello\" to my little ______!\" Scarface, 1983" },
            new WordAndDefinition { Word = "DUMP", Definition = "\"What a ____.\" Beyond the Forest, 1949" },
            new WordAndDefinition { Word = "SEDUCE", Definition = "\"Mrs. Robinson, you're trying to ______ me. Aren't you?\" The Graduate, 1967" },
            new WordAndDefinition { Word = "WAR", Definition = "\"Gentlemen, you can't fight in here! This is the ___ Room!\" Dr. Strangelove, 1964" },
            new WordAndDefinition { Word = "ELEMENTARY", Definition = "\"__________, my dear Watson.\" The Adventures of Sherlock Holmes, 1929" },
            new WordAndDefinition { Word = "APE", Definition = "\"Get your stinking paws off me, you damned dirty ___.\" Planet of the Apes, 1968" },
            new WordAndDefinition { Word = "GIN", Definition = "\"Of all the ___ joints in all the towns in all the world, she walks into mine.\" Casablanca, 1942" },
            new WordAndDefinition { Word = "JOHNNY", Definition = "\"Here's ______!\" The Shining, 1980" },
            new WordAndDefinition { Word = "SAFE", Definition = "\"Is it ____?\" Marathon Man, 1976" },
            new WordAndDefinition { Word = "MINUTE", Definition = "\"Wait a ______, wait a ______. You ain't heard nothin' yet!\" The Jazz Singer, 1927" },
            new WordAndDefinition { Word = "HANGERS", Definition = "\"No wire _______, ever!\" Mommie Dearest, 1981" },
            new WordAndDefinition { Word = "RICO", Definition = "\"Mother of mercy, is this the end of ____?\" Little Caesar, 1930" },
            new WordAndDefinition { Word = "CHINATOWN", Definition = "\"Forget it, Jake, it's _________.\" Chinatown, 1974" },
            new WordAndDefinition { Word = "STRANGERS", Definition = "\"I have always depended on the kindness of _________.\" A Streetcar Named Desire, 1951Terminator saying hasta la vista baby" },
            new WordAndDefinition { Word = "BABY", Definition = "\"Hasta la vista, ____.\" Terminator 2: Judgment Day, 1991​​​​​" },
            new WordAndDefinition { Word = "PEOPLE", Definition = "\"Soylent Green is people!\" Soylent Green, 1973" },
            new WordAndDefinition { Word = "POD", Definition = "\"Open the ___ bay doors, HAL.\" 2001: A Space Odyssey, 1968" },
            new WordAndDefinition { Word = "ADRIAN", Definition = "\"Yo, _____!\" Rocky, 1976Rocky saying yo Adrian" },
            new WordAndDefinition { Word = "GORGEOUS", Definition = "\"Hello, ________.\" Funny Girl, 1968" },
            new WordAndDefinition { Word = "NIGHT", Definition = "\"Listen to them. Children of the _____. What music they make.\" Dracula, 1931" },
            new WordAndDefinition { Word = "KILLED", Definition = "\"Oh, no, it wasn't the airplanes. It was Beauty ______ the Beast.\" King Kong, 1933" },
            new WordAndDefinition { Word = "PRECIOUS", Definition = "\"My ________.\" The Lord of the Rings: Two Towers, 2002" },
            new WordAndDefinition { Word = "STAR", Definition = "\"Sawyer, you're going out a youngster, but you've got to come back a ____!\" 42nd Street, 1933" },
            new WordAndDefinition { Word = "ARMOR", Definition = "\"Listen to me, mister. You're my knight in shining _____. ...\" On Golden Pond, 1981" },
            new WordAndDefinition { Word = "WIN", Definition = "\"Tell 'em to go out there with all they got and ___ just one for the Gipper.\" Knute Rockne All American, 1940" },
            new WordAndDefinition { Word = "MARTINI", Definition = "\"A _______. Shaken, not stirred.\" Goldfinger, 1964" },
            new WordAndDefinition { Word = "FIRST", Definition = "\"Who's on _____.\" The Naughty Nineties, 1945" },
            new WordAndDefinition { Word = "SUCKERS", Definition = "\"Life is a banquet, and most poor _______ are starving to death!\" Auntie Mame, 1958" },
            new WordAndDefinition { Word = "SPEED", Definition = "\"I feel the need - the need for _____!\" Top Gun, 1986" },
            new WordAndDefinition { Word = "DAY", Definition = "\"Carpe diem. Seize the ___, boys. Make your lives extraordinary.\" Dead Poets Society, 1989" },
            new WordAndDefinition { Word = "SNAP", Definition = "\"____ out of it!\" Moonstruck, 1987" },
            new WordAndDefinition { Word = "MOTHER", Definition = "\"My ______ thanks you. My father thanks you. My sister thanks you. And I thank you.\" Yankee Doodle Dandy, 1942" },
            new WordAndDefinition { Word = "CORNER", Definition = "\"Nobody puts Baby in a ______.\" Dirty Dancing, 1987" },
            new WordAndDefinition { Word = "PRETTY", Definition = "\"I'll get you, my _____, and your little dog, too!\" The Wizard of Oz, 1939Jack Dawson, Titanic, yelling I'm the King of the world" },
            new WordAndDefinition { Word = "KING", Definition = "\"I'm ____ of the world!\" Titanic, 1997" },











































































































        };

    }

}
