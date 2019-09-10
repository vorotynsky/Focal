// Copyright 2019 Vorotynsky Maxim
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Focal
    module Resource = begin

        /// Creates localizator based on resource manager.
        val createLocalizator :
            resourseManager:System.Resources.ResourceManager ->
            Localizator<string,string>

        /// Creates localizator based on resources of assembley.
        val createAssembleyLocalizator :
            assembly:System.Reflection.Assembly -> Localizator<string,string>
    end


