/*
 * QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals.
 * Lean Algorithmic Trading Engine v2.0. Copyright 2014 QuantConnect Corporation.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/

using System;
using System.Collections.Generic;

namespace QuantConnect.Algorithm.Framework.Alphas.Analysis
{
    /// <summary>
    /// Encapsulates the storage and on-line scoring of insights.
    /// </summary>b
    public interface IInsightManager : IDisposable
    {
        /// <summary>
        /// Enumerable of insights still under analysis
        /// </summary>
        IEnumerable<Insight> OpenInsights { get; }

        /// <summary>
        /// Enumerable of insights who's analysis has been completed
        /// </summary>
        IEnumerable<Insight> ClosedInsights { get; }

        /// <summary>
        /// Enumerable of all internally maintained insights
        /// </summary>
        IEnumerable<Insight> AllInsights { get; }

        /// <summary>
        /// Add an extension to this manager
        /// </summary>
        /// <param name="extension">The extension to be added</param>
        void AddExtension(IInsightManagerExtension extension);

        /// <summary>
        /// Initializes any extensions for the specified backtesting range
        /// </summary>
        /// <param name="start">The start date of the backtest (current time in live mode)</param>
        /// <param name="end">The end date of the backtest (<see cref="Time.EndOfTime"/> in live mode)</param>
        /// <param name="current">The algorithm's current utc time</param>
        void InitializeExtensionsForRange(DateTime start, DateTime end, DateTime current);

        /// <summary>
        /// Steps the manager forward in time, accepting new state information and potentialy newly generated insights
        /// </summary>
        /// <param name="frontierTimeUtc">The frontier time of the insight analysis</param>
        /// <param name="securityValuesCollection">Snap shot of the securities at the frontier time</param>
        /// <param name="generatedInsights">Any insight generated by the algorithm at the frontier time</param>
        void Step(DateTime frontierTimeUtc, ReadOnlySecurityValuesCollection securityValuesCollection, GeneratedInsightsCollection generatedInsights);

        /// <summary>
        /// Removes insights from the manager with the specified ids
        /// </summary>
        /// <param name="insightIds">The insights ids to be removed</param>
        void RemoveInsights(IEnumerable<Guid> insightIds);
    }
}
