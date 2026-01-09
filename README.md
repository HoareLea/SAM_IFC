[![Build (Windows)](https://github.com/SAM-BIM/SAM_IFC/actions/workflows/build.yml/badge.svg?branch=master)](https://github.com/SAM-BIM/SAM_IFC/actions/workflows/build.yml)
[![Installer (latest)](https://img.shields.io/github/v/release/SAM-BIM/SAM_Deploy?label=installer)](https://github.com/SAM-BIM/SAM_Deploy/releases/latest)

# SAM_IFC

<a href="https://github.com/SAM-BIM/SAM">
  <img src="https://github.com/SAM-BIM/SAM/blob/master/Grasshopper/SAM.Core.Grasshopper/Resources/SAM_Small.png"
       align="left" hspace="10" vspace="6">
</a>

**SAM_IFC** is part of the **SAM (Sustainable Analytical Model) Toolkit** —  
an open-source collection of tools designed to help engineers create, manage,
and process analytical building models for energy and environmental analysis.

This repository provides **IFC export utilities for SAM analytical models**
using the **xBIM Toolkit**.
It enables analytical model data created within SAM workflows
to be translated into Industry Foundation Classes (IFC) format
for interoperability with BIM authoring and coordination tools.

The export focuses on representing analytical and derived building information
in a structured IFC form suitable for coordination, exchange, and downstream use.

Welcome — and let’s keep the open-source journey going. 🤝

---

## Features

- Export of SAM analytical models to IFC format
- Use of the **xBIM Toolkit** for IFC generation
- Mapping of analytical elements to IFC entities
- Support for interoperability with BIM tools and workflows

---

## References

- 🧱 **xBIM Toolkit:** https://docs.xbim.net/  
- 🏗 **IFC (buildingSMART):** https://technical.buildingsmart.org/  

---

## Resources
- 📘 **SAM Wiki:** https://github.com/SAM-BIM/SAM/wiki  
- 🧠 **SAM Core:** https://github.com/SAM-BIM/SAM  

---

## Installing

To install **SAM** using the Windows installer, download and run the  
[latest installer](https://github.com/SAM-BIM/SAM_Deploy/releases/latest).

Alternatively, you can build the toolkit from source using Visual Studio.  
See the main repository for details:  
👉 https://github.com/SAM-BIM/SAM

---

## Development notes

- Target framework: **.NET / C#**
- IFC export follows SAM-BIM analytical modelling conventions
- IFC generation is implemented using the xBIM Toolkit
- New or modified `.cs` files must include the SPDX header from `COPYRIGHT_HEADER.txt`

---

## Licence

This repository is free software licensed under the  
**GNU Lesser General Public License v3.0 or later (LGPL-3.0-or-later)**.

Each contributor retains copyright to their respective contributions.  
The project history (Git) records authorship and provenance of all changes.

See:
- `LICENSE`
- `NOTICE`
- `COPYRIGHT_HEADER.txt`
