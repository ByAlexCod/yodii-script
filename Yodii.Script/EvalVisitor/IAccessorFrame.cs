#region LGPL License
/*----------------------------------------------------------------------------
* This file (Yodii.Script\EvalVisitor\IAccessorFrame.cs) is part of Yodii-Script. 
*  
* Yodii-Script is free software: you can redistribute it and/or modify 
* it under the terms of the GNU Lesser General Public License as published 
* by the Free Software Foundation, either version 3 of the License, or 
* (at your option) any later version. 
*  
* Yodii-Script is distributed in the hope that it will be useful, 
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the 
* GNU Lesser General Public License for more details. 
* You should have received a copy of the GNU Lesser General Public License 
* along with Yodii-Script. If not, see <http://www.gnu.org/licenses/>. 
*  
* Copyright © 2007-2015, 
*     Invenietis <http://www.invenietis.com>, IN'TECH INFO <http://www.intechinfo.fr>
* All rights reserved. 
*-----------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;


namespace Yodii.Script
{

    /// <summary>
    /// Encapsulates chain of accessors.
    /// </summary>
    public interface IAccessorFrame
    {
        /// <summary>
        /// Gets the <see cref="AccessorExpr"/> of this frame.
        /// </summary>
        AccessorExpr Expr { get; }

        /// <summary>
        /// Gets the global context.
        /// </summary>
        GlobalContext Global { get; }

        /// <summary>
        /// Gets the next accessor if any.
        /// </summary>
        IAccessorFrame NextAccessor { get; }

        /// <summary>
        /// Resolves this frame and returns a resolved promise.
        /// </summary>
        /// <param name="result">The evaluated resulting object.</param>
        /// <returns>A resolved promise.</returns>
        PExpr SetResult( RuntimeObj result );

        /// <summary>
        /// Resolves this frame with an error and returns a resolved promise.
        /// </summary>
        /// <param name="message">
        /// An optional error message. When let to null, a default message describing 
        /// the error is generated ("unknown property: x." for example) with 
        /// the <see cref="RuntimeError.IsReferenceError"/> sets to true.
        /// </param>
        PExpr SetError( string message = null );

        /// <summary>
        /// Gets whether this frame has been resolved: either <see cref="SetError"/> 
        /// or <see cref="SetResult"/> has been called.
        /// </summary>
        bool IsResolved { get; }

        /// <summary>
        /// Initializes an accessor state based on a configuration. 
        /// Returns null if no matching configuration have been found.
        /// </summary>
        /// <param name="configuration">Configuration of resolution handlers.</param>
        /// <returns>Null if no matching configuration have been found.</returns>
        IAccessorFrameState GetImplementationState( Action<IAccessorFrameInitializer> configuration );

        /// <summary>
        /// Initializes an accessor state for a call.
        /// Once arguments are resolved, the <paramref name="call"/> itself is made.
        /// </summary>
        /// <param name="arguments">Arguments of the call.</param>
        /// <param name="call">Function to call.</param>
        /// <returns>The frame state that encapsulates the parameters resolution and the call.</returns>
        IAccessorFrameState GetCallState( IReadOnlyList<Expr> arguments, Func<IAccessorFrame, IReadOnlyList<RuntimeObj>, PExpr> call );

    }

}
