// Tested.h
#pragma once
#include <string>
#include <vector>
#include "Category.h"
#include "nlohmann/json.hpp"
using json = nlohmann::json;

class Tested {
private:
    std::string name_;
    std::vector<Category*> available_;
    int total_points_ = 0;
    int passed_categories_ = 0;

public:
    Tested();
    explicit Tested(const std::string& name);

    const std::string& getName() const;
    void setName(const std::string& name);

    int getTotalPoints() const;
    int getPassedCategories() const;

    void setAvailableCategories(const std::vector<Category*>& cats);
    const std::vector<Category*>& getAvailableCategories() const;
    void addAvailableCategory(Category* cat);

    int startCategory(Category& category);
    double getAverage() const;
    void printResults() const;

    json toJson() const;
    void fromJson(const json& j);
    bool saveResultsToFile(const std::string& filename) const;
    bool loadResultsFromFile(const std::string& filename);
};
