![shipfer-banner-image](https://github.com/ZadokJoshua/shipfer/assets/65626254/504f5dbd-412b-44b6-a8ae-52455583b335)

<b>Shipfer</b> is a project designed to revolutionize the shipping process for small businesses and online sellers. By utilizing AI solutions and real-time data integration from the Pitney Bowes Shipping 360 API, Shipfer aims to create a seamless and efficient shipping experience. Our platform provides users with carrier rates suggestions, ensuring that every shipment is cost-effective, and easily managed. Shipfer is not just an app; it’s a comprehensive solution to streamline logistics, reduce costs, and enhance customer satisfaction, making the complexities of shipping simple and accessible for everyone.

![image](https://github.com/ZadokJoshua/shipfer/assets/65626254/0f6f65c8-cc5b-4cd5-8808-c858e5f5317f)

## Technical Implementation
### Tools and Technologies used
C#, Visual Studio 2022, PyCharm, .NET MAUI, MVVM Community Toolkit, Uranium UI.

### Services
- Azure AI Bot Service
- AI Language Service
- Supabase
- Text to Speech
- Speech to Text
  
### User Flow
1. User Initiates a Query:
The user types in a query, such as "I want to send a package of weight 3 kg with dimensions 14x9x7 to this Destination address 456 Pine ST, CA, 94016".
2. Query Sent to Azure Conversational Language Understanding project:
The query is sent to the Azure CLU project created in the Azure AI language studio.
3. Intent and Entities Extraction:
Azure AI processes the query to extract intents and entities. The top intent is identified, e.g., "GetFastestOptionShippingRates". Relevant entities such as weight, dimensions, and destination address are extracted.
4. Call Function Based on Top Intent:
A function that corresponds to the top intent is identified and called. For example, if the top intent is "GetFastestOptionShippingRates", the function GetFastestOptionShippingRates() is called.
5. Process Intent in Backend:
The function GetFastestOptionShippingRates() processes the intent and validates the extracted entities.
6. Retrieve Shipping Rates:
The app interacts with Pitney Bowes Shipping 360 API to retrieve shipping rates. It arranges the rates according to the nearest expected delivery time.
7. Format and Send Response:
The app formats the response with the arranged shipping rates.
8. Display Response to User

## Next Steps
### AI Orchestrator Implementation
We plan to leverage Semantic Kernel to develop an advanced AI agent for Shipfer, enhancing natural language processing and decision-making capabilities. This integration will drive intelligent shipping cost optimization and smarter carrier rate suggestions, streamlining the shipping process for our users.

### Integration with Online Marketplaces
Our future plans include integrating Shipfer with leading online marketplaces like Amazon, eBay, and Etsy. This will enable seamless synchronization of orders and automated shipping solutions, providing a comprehensive and efficient shipping management system for small businesses and online sellers.

## Team
- [David Gethers](https://github.com/dgethers)
- [Zadok Joshua](https://github.com/ZadokJoshua/)

## Resources 
Here's the link to our [presentation slides](Documents/shipfer-presentation-slides.pdf) and [video](https://drive.google.com/file/d/19zWrZeGmFy8_zjKggG1EuSeiVn60yjoj/view?usp=sharing)
