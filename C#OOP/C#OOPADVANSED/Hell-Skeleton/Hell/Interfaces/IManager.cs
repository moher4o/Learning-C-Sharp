
using System;
using System.Collections.Generic;

public interface IManager
{
    string AddHero(IList<String> arguments);

    string AddItemToHero(IList<String> arguments);

    string AddRecipeToHero(IList<String> arguments);

    string CreateGame();

    string Inspect(IList<String> arguments);

    string Quit();
}
