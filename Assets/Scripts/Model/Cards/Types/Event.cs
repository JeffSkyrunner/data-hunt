﻿namespace model.cards.types
{
    public class Event : IType
    {
        bool IType.Playable => true;
        bool IType.Installable => false;
        bool IType.Rezzable => false;
    }
}