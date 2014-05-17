﻿/*
* The Poison: discrete event simulation system.
* Copyright (C) 2013-2014 Poison team.
*
* This program is free software: you can redistribute it and/or modify
* it under the terms of the GNU General Public License as published by
* the Free Software Foundation, either version 3 of the License, or
* (at your option) any later version.
*
* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
* GNU General Public License for more details.
*
* You should have received a copy of the GNU General Public License
* along with this program. If not, see <http://www.gnu.org/licenses/>.
*/

namespace Poison.Modelling
{
    public delegate void EventHandler(object param);
    public delegate void EventHandler<in T>(T obj);
    public delegate void EventHandler<in T1, in T2>(T1 obj1, T2 obj2);
    public delegate void EventHandler<in T1, in T2, in T3>(T1 obj1, T2 obj2, T3 obj3);
}
